using System;
using System.ComponentModel;
using System.Data.Linq;

namespace HS.PasteBin.Core
{
    public partial class Paste : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs =
            new PropertyChangingEventArgs(String.Empty);

        private DateTime createdAt;
        private string html;
        private int id;

        private string language;
        private string preview;
        private string text;

        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnTextChanging(string value);
        partial void OnTextChanged();
        partial void OnHtmlChanging(string value);
        partial void OnHtmlChanged();
        partial void OnPreviewChanging(string value);
        partial void OnPreviewChanged();
        partial void OnCreatedAtChanging(DateTime value);
        partial void OnCreatedAtChanged();
        partial void OnLanguageChanging(string value);
        partial void OnLanguageChanged();

        #endregion

        public Paste()
        {
            OnCreated();
        }

        public int Id
        {
            get { return id; }
            set
            {
                if ((id != value))
                {
                    OnIdChanging(value);
                    SendPropertyChanging();
                    id = value;
                    SendPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if ((text != value))
                {
                    OnTextChanging(value);
                    SendPropertyChanging();
                    text = value;
                    SendPropertyChanged("Text");
                    OnTextChanged();
                }
            }
        }

        public string Html
        {
            get { return html; }
            set
            {
                if ((html != value))
                {
                    OnHtmlChanging(value);
                    SendPropertyChanging();
                    html = value;
                    SendPropertyChanged("Html");
                    OnHtmlChanged();
                }
            }
        }

        public string Preview
        {
            get { return preview; }
            set
            {
                if ((preview != value))
                {
                    OnPreviewChanging(value);
                    SendPropertyChanging();
                    preview = value;
                    SendPropertyChanged("Preview");
                    OnPreviewChanged();
                }
            }
        }

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set
            {
                if ((createdAt != value))
                {
                    OnCreatedAtChanging(value);
                    SendPropertyChanging();
                    createdAt = value;
                    SendPropertyChanged("CreatedAt");
                    OnCreatedAtChanged();
                }
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                if ((language != value))
                {
                    OnLanguageChanging(value);
                    SendPropertyChanging();
                    language = value;
                    SendPropertyChanged("Language");
                    OnLanguageChanged();
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, EmptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}