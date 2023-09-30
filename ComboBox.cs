using System;
using System.Collections;
using Xamarin.Forms;
using static Xamarin.Forms.VisualMarker;

namespace Xamarin.Forms.ComboBox
{
    /// <summary>
    /// Combo box with search option
    /// </summary>
    public class ComboBox : StackLayout
    {
        private Entry _entry;
        private Label _errorMessage;
        private ListView _listView;
        private StackLayout stackLayout;
        private bool _supressFiltering;
        private bool _supressSelectedItemFiltering;
        public double EntryHeigth
        {
            get => _entry.Height;
        }

        public bool IsListViewVisible
        {
            get => _listView.IsVisible;
        }


        //Bindable properties
        public static readonly BindableProperty ErrorMessageEnableProperty = BindableProperty.Create(nameof(ErrorMessageEnable), typeof(bool), typeof(ComboBox), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            bool nuevo = (bool)newVal;
        });

        public bool ErrorMessageEnable
        {
            get { return (bool)GetValue(ErrorMessageEnableProperty); }
            set { SetValue(ErrorMessageEnableProperty, value); }
        }

        public static readonly BindableProperty ErrorMessageTextProperty = BindableProperty.Create(nameof(ErrorMessageText), typeof(string), typeof(ComboBox), defaultValue: string.Empty, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._errorMessage.Text = (string)newVal;
        });

        public string ErrorMessageText
        {
            get { return (string)GetValue(ErrorMessageTextProperty); }
            set { SetValue(ErrorMessageTextProperty, value); }
        }

        public static readonly BindableProperty EntryHeightRequestProperty = BindableProperty.Create(nameof(EntryHeightRequest), typeof(double), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._entry.HeightRequest = (double)newVal;
        });

        public double EntryHeightRequest
        {
            get { return (double)GetValue(EntryHeightRequestProperty); }
            set { SetValue(EntryHeightRequestProperty, value); }
        }

        public static readonly BindableProperty EntryFontSizeProperty = BindableProperty.Create(nameof(EntryFontSize), typeof(double), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._entry.FontSize = (double)newVal;
        });

        [TypeConverter(typeof(FontSizeConverter))]
        public double EntryFontSize
        {
            get { return (double)GetValue(EntryFontSizeProperty); }
            set { SetValue(EntryFontSizeProperty, value); }
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._entry.PlaceholderColor = (Color)newVal;
        });

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public static readonly BindableProperty ListViewBackgroundColorProperty = BindableProperty.Create(nameof(ListViewBackgroundColor), typeof(Color), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._listView.BackgroundColor = (Color)newVal;
            comboBox.stackLayout.BackgroundColor = (Color)newVal;
        });

        public Color ListViewBackgroundColor
        {
            get { return (Color)GetValue(ListViewBackgroundColorProperty); }
            set { SetValue(ListViewBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty EntryBackgroundColorProperty = BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._entry.BackgroundColor = (Color)newVal;
        });

        public Color EntryBackgroundColor
        {
            get { return (Color)GetValue(EntryBackgroundColorProperty); }
            set { SetValue(EntryBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty ListViewHeightRequestProperty = BindableProperty.Create(nameof(ListViewHeightRequest), typeof(double), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._listView.HeightRequest = (double)newVal;
        });

        public double ListViewHeightRequest
        {
            get { return (double)GetValue(ListViewHeightRequestProperty); }
            set { SetValue(ListViewHeightRequestProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._listView.ItemsSource = (IEnumerable)newVal;
        });

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._listView.SelectedItem = newVal;
        });

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static new readonly BindableProperty VisualProperty = BindableProperty.Create(nameof(Visual), typeof(IVisual), typeof(ComboBox), defaultValue: new DefaultVisual(), propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._listView.Visual = (IVisual)newVal;
            comboBox._entry.Visual = (IVisual)newVal;
        });

        public new IVisual Visual
        {
            get { return (IVisual)GetValue(VisualProperty); }
            set { SetValue(VisualProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ComboBox), defaultValue: "", propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._entry.Placeholder = (string)newVal;
        });

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ComboBox), defaultValue: "", propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._entry.Text = (string)newVal;
        });

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ComboBox), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) => {
            var comboBox = (ComboBox)bindable;
            comboBox._listView.ItemTemplate = (DataTemplate)newVal;
        });

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty EntryDisplayPathProperty = BindableProperty.Create(nameof(EntryDisplayPath), typeof(string), typeof(ComboBox), defaultValue: "");

        public string EntryDisplayPath
        {
            get { return (string)GetValue(EntryDisplayPathProperty); }
            set { SetValue(EntryDisplayPathProperty, value); }
        }

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        protected virtual void OnSelectedItemChanged(SelectedItemChangedEventArgs e)
        {
            EventHandler<SelectedItemChangedEventArgs> handler = SelectedItemChanged;
            handler?.Invoke(this, e);
        }

        public event EventHandler<TextChangedEventArgs> TextChanged;

        protected virtual void OnTextChanged(TextChangedEventArgs e)
        {
            EventHandler<TextChangedEventArgs> handler = TextChanged;
            handler?.Invoke(this, e);
        }


        public ComboBox()
        {
            CascadeInputTransparent = false;
            InputTransparent = true;
            //Entry used for filtering list view
            _entry = new Entry();
            _entry.Margin = new Thickness(0);
            _entry.Keyboard = Keyboard.Create(KeyboardFlags.None);
            _entry.Focused += (sender, args) =>
            {
                _listView.IsVisible = true;
                _errorMessage.IsVisible = ErrorMessageEnable;
                //_errorMessage.IsVisible = true;
                stackLayout.IsVisible = true;
            };
            _entry.Unfocused += (sender, args) =>
            {
                _listView.IsVisible = false;
                _errorMessage.IsVisible = false;
                stackLayout.IsVisible = false;
            };

            //Text changed event, bring it back to the surface
            _entry.TextChanged += (sender, args) =>
            {
                if (_supressFiltering)
                    return;

                if (String.IsNullOrEmpty(args.NewTextValue))
                {
                    _supressSelectedItemFiltering = true;
                    _listView.SelectedItem = null;
                    _supressSelectedItemFiltering = false;
                }

                _listView.IsVisible = true;

                OnTextChanged(args);
            };

            _errorMessage = new Label();
            _errorMessage.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label), useOldSizes: false);
            _errorMessage.TextColor = Color.Red;
            _errorMessage.IsVisible = false;
            _errorMessage.HorizontalTextAlignment = TextAlignment.Center;
            _errorMessage.Margin = new Thickness(0);

            //List view - used to display search options
            _listView = new ListView();
            _listView.Margin = new Thickness(0);
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.ListView.SetSeparatorStyle(_listView, Xamarin.Forms.PlatformConfiguration.iOSSpecific.SeparatorStyle.FullWidth);
            _listView.HeightRequest = 100;
            _listView.HorizontalOptions = LayoutOptions.StartAndExpand;
            //_listView.IsVisible = false;
            _listView.SetBinding(ListView.SelectedItemProperty, new Binding(nameof(ComboBox.SelectedItem), source: this));
            _listView.RowHeight = 49;

            //Item selected event, surface it back to the top
            _listView.ItemSelected += (sender, args) =>
            {
                if (!_supressSelectedItemFiltering)
                {
                    _supressFiltering = true;

                    var selectedItem = args.SelectedItem;
                    _entry.Text = !String.IsNullOrEmpty(EntryDisplayPath) && selectedItem != null ? selectedItem.GetType().GetProperty(EntryDisplayPath).GetValue(selectedItem, null).ToString() : selectedItem?.ToString();
                    //SelectedItem = args.SelectedItem;

                    _supressFiltering = false;
                    _listView.IsVisible = false;
                    OnSelectedItemChanged(args);
                    _entry.Unfocus();
                }
            };

            //Add bottom border
            var boxView = new BoxView();
            boxView.HeightRequest = 1;
            boxView.Color = Color.Black;
            boxView.Margin = new Thickness(0);
            //boxView.SetBinding(BoxView.IsVisibleProperty, new Binding(nameof(ListView.IsVisible), source: _listView));

            stackLayout = new StackLayout();
            stackLayout.Orientation = StackOrientation.Vertical;
            stackLayout.InputTransparent = true;
            stackLayout.CascadeInputTransparent = false;
            stackLayout.IsVisible = false;
            stackLayout.Children.Add(_errorMessage);
            stackLayout.Children.Add(_listView);
            stackLayout.Children.Add(boxView);

            Children.Add(_entry);
            //Children.Add(_errorMessage);
            //Children.Add(_listView);
            //Children.Add(boxView);
            Children.Add(stackLayout);
        }

        public new bool Focus()
        {
            return _entry.Focus();
        }

        public new void Unfocus()
        {
            _entry.Unfocus();
        }
    }
}
