using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HS.PasteBin.Core;
using HS.PasteBin.Web.Controllers;
using HS.PasteBin.Web.Exceptions;
using HS.PasteBin.Web.ViewModels;
using HS.SyntaxHighlighting;
using NUnit.Framework;

namespace HS.PasteBin.Web.Tests
{
    [TestFixture]
    public class PasteControllerFixture
    {
        [Test]
        public void CreateReturnsLanguagesGiven()
        {
            var config = new Config {DefaultLanguageKeys = new[] {"a"}};

            var otherLanguages = new Dictionary<string, string> {{"a", "A"}, {"b", "B"}};

            var controller = new PasteController(null, new MockHighlighter(otherLanguages), config);

            var model = ((LanguageSelection) ((ViewResult) controller.Create()).ViewData.Model);

            Assert.AreEqual(new Language("a", "A"), model.DefaultLanguages.Single());
            Assert.AreEqual(otherLanguages, model.OtherLanguages);
        }

        [Test]
        public void ShowReturnsPasteInfoForPasteRequested()
        {
            var paste = new Paste
                            {
                                Id = 42,
                                CreatedAt = DateTime.MinValue,
                                Html = "html",
                                Language = "a"
                            };

            var language = new Language("a", "A");

            var controller = new PasteController
                (
                new MockPasteRepository(new[] { paste }, new DeterminateDateStamper(DateTime.Now)),
                new MockHighlighter(new Dictionary<string, string> { { language.Key, language.Name } }),
                new Config { DefaultLanguageKeys = new [] { language.Key } }
                );

            Assert.AreEqual
                (
                new PasteInfo
                    {
                        ID = paste.Id,
                        CreatedAt = paste.CreatedAt,
                        Html = paste.Html,
                        Language = language.Name
                    },
                ((ViewResult) controller.Show(paste.Id)).ViewData.Model
                );
        }

        [Test, ExpectedException(typeof (PasteNotFoundException))]
        public void ShowThrowsPasteNotFoundWhenAppropriate()
        {
            new PasteController
                (
                new MockPasteRepository(new Paste[] {}, new DeterminateDateStamper(DateTime.Now)),
                new MockHighlighter(new Dictionary<string, string>()),
                new Config { DefaultLanguageKeys = new string [] { } }
                )
                .Show(42);
        }

        [Test]
        public void ShowWouldRedirectToPasteNotFoundWhenAppropriate()
        {
            Assert.AreEqual
                (
                "PasteNotFound",
                typeof (PasteController)
                    .GetMethod("Show")
                    .GetCustomAttributes(false /* Ignore inherited */)
                    .Cast<HandleErrorAttribute>()
                    .Single()
                    .View
                );
        }

        [Test, ExpectedException(typeof(LanguageUnknownException))]
        public void SaveFailsWhenInvalidLanguageKeyGiven()
        {
            var controller = new PasteController
            (
                new MockPasteRepository(new Paste[] { }, new DeterminateDateStamper(DateTime.Now)),
                new MockHighlighter(new Dictionary<string, string>()),
                new Config { DefaultLanguageKeys = new string [] { } }
            );

            controller.Save("", "", "");
        }

        [Test, ExpectedException(typeof(LanguageUnknownException))]
        public void SaveFailsWhenInvalidOtherLanguageKeyGiven()
        {
            var controller = new PasteController
            (
                new MockPasteRepository(new Paste[] { }, new DeterminateDateStamper(DateTime.Now)),
                new MockHighlighter(new Dictionary<string, string>()),
                new Config { DefaultLanguageKeys = new string [] { } }
            );

            controller.Save("", Constants.RequestKey.LanguageOther, "");
        }

        [Test]
        public void ShowWouldRedirectToLanguageUnknownWhenAppropriate()
        {
            Assert.AreEqual
                (
                "LanguageUnknown",
                typeof (PasteController)
                    .GetMethod("Save")
                    .GetCustomAttributes(false /* Ignore inherited */)
                    .Cast<HandleErrorAttribute>()
                    .Single()
                    .View
                );
        }

        [Test]
        public void RecentReturnsNoPastesWhenAskedForNone()
        {
            var paste = new Paste
            {
                Id = 42,
                CreatedAt = DateTime.MinValue,
                Html = "html",
                Language = "a"
            };

            var date = new DateTime(2000, 1, 1);

            var language = new Language("a", "A");

            var controller = new PasteController
            (
                new MockPasteRepository(new[] { paste }, new DeterminateDateStamper(date)),
                new MockHighlighter(new Dictionary<string, string> { { language.Key, language.Name } }),
                new Config { DefaultLanguageKeys = new string[] { } }
            );

            Assert.IsFalse
                (((IEnumerable<RecentPasteInfo>)((ViewResult)controller.Recent()).ViewData.Model).Any());
        }

        [Test]
        public void RecentReturnsOnlyExistingPasteAsRecentPasteInfoWhenAskedForTwo()
        {
            var date = new DateTime(2000, 1, 1);

            var paste = new Paste
                            {
                                Id = 42,
                                CreatedAt = date,
                                Html = "html",
                                Preview = "a|preview",
                                Language = "a"
                            };

            var language = new Language("a", "A");

            var controller = new PasteController
            (
                new MockPasteRepository(new [] { paste }, new DeterminateDateStamper(date)),
                new MockHighlighter(new Dictionary<string, string> {{language.Key, language.Name}}),
                new Config { DefaultLanguageKeys = new string [] { }, MaxPastesInRecentView = 2 }
            );

            Assert.AreEqual
                (
                    new RecentPasteInfo { CreatedAt = date, ID = paste.Id, LanguageName = "A", Preview = "a|preview" },
                    ((IEnumerable<RecentPasteInfo>)((ViewResult)controller.Recent()).ViewData.Model).Single()
                );
        }

        [Test]
        public void RecentReturnsOnlyOnePasteAsRecentPasteInfoWhenAskedForOne()
        {
            var date = new DateTime(2000, 1, 1);
            var date2 = new DateTime(2000, 1, 2);

            var paste = new Paste
                            {
                                Id = 42,
                                CreatedAt = date,
                                Html = "html",
                                Preview = "a|preview",
                                Language = "a"
                            };

            var paste2 = new Paste
                            {
                                Id = 43,
                                CreatedAt = date2,
                                Html = "html",
                                Preview = "a|preview",
                                Language = "a"
                            };

            var language = new Language("a", "A");

            var controller = new PasteController
            (
                new MockPasteRepository(new [] { paste, paste2 }, new DeterminateDateStamper(date)),
                new MockHighlighter(new Dictionary<string, string> {{language.Key, language.Name}}),
                new Config { DefaultLanguageKeys = new string [] { }, MaxPastesInRecentView = 1 }
            );

            Assert.AreEqual
                (
                    new RecentPasteInfo { CreatedAt = date2, ID = paste2.Id, LanguageName = "A", Preview = "a|preview" },
                    ((IEnumerable<RecentPasteInfo>)((ViewResult)controller.Recent()).ViewData.Model).Single()
                );
        }
    }
}