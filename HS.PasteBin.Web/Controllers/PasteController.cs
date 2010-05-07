using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HS.PasteBin.Model;
using HS.PasteBin.Web.Exceptions;
using HS.PasteBin.Web.ViewModels;
using HS.SyntaxHighlighting;

namespace HS.PasteBin.Web.Controllers
{
    public class PasteController : Controller 
    {
        private readonly IHighlighter highlighter;
        private readonly IPasteRepository repository;
        private Config config;

        public PasteController
            (
            IPasteRepository repository,
            IHighlighter highlighter,
            Config config
            )
        {
            this.repository = repository;
            this.highlighter = highlighter;
            this.config = config;
        }

        public ActionResult Create()
        {
            return View
                (
                new LanguageSelection
                    {
                        DefaultLanguages = config.DefaultLanguageKeys.Select
                            (key => new Language(key, highlighter.Languages[key])),
                        OtherLanguages = highlighter.Languages
                    }
                );
        }

        [HandleError(ExceptionType = typeof (KeyNotFoundException), View = "PasteNotFound")]
        public ActionResult Show(int id)
        {
            try
            {
                var paste = repository.Find(id);

                return View
                    (
                    new PasteInfo
                        {
                            ID = paste.Id,
                            CreatedAt = paste.CreatedAt,
                            Language = highlighter.Languages[paste.Language],
                            Html = paste.Html
                        }
                    );
            }
            catch (Exception)
            {
                throw new PasteNotFoundException(id);
            }
        }

        [HandleError(ExceptionType = typeof(LanguageUnknownException), View = "LanguageUnknown")]
        public ActionResult Save(string text, string language, string languageOther)
        {
            string languageKey = language == Constants.RequestKey.LanguageOther ? languageOther : language;

            if (!highlighter.Languages.ContainsKey(languageKey))
                throw new LanguageUnknownException(languageKey);

            string html = highlighter.Highlight(text, languageKey);
            string preview = highlighter.Highlight(Left(text, config.MaxPreviewCharacters), languageKey);

            int id = repository.Create(text, html, preview, languageKey);

            return RedirectToAction("Show", new {ID = id});
        }

        public ActionResult Recent()
        {
            return View
                (
                repository.Last(config.MaxPastesInRecentView).Select
                    (
                    paste =>
                    new RecentPasteInfo
                        {
                            ID = paste.Id,
                            CreatedAt = paste.CreatedAt,
                            Preview = paste.Preview,
                            LanguageName = highlighter.Languages[paste.Language]
                        }
                    )
                );
        }

        private static string Left(string s, int maxCharacterCount)
        {
            return s.Substring(0, Math.Min(s.Length, maxCharacterCount));
        }
    }
}