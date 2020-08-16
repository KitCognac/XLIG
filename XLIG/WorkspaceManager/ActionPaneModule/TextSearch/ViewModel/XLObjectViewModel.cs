using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLIG.WorkspaceManager.ActionPaneModule.TextSearch
{
    public class XLObjectViewModel : INotifyPropertyChanged
    {
        #region Data

        public XLObjectViewModel Parent { get; }
        readonly XLObject _xlobject;

        #region XLObjectProperties

        public ReadOnlyCollection<XLObjectViewModel> Children { get; }
        public string Name => _xlobject.Type + " - " + _xlobject.Name;

        #endregion //XLObjectProperties

        bool _isExpanded;
        bool _isSelected;

        #endregion //Data

        #region Constructor

        public XLObjectViewModel(XLObject xlobj)
        : this(xlobj, null)
        {
        }
        private XLObjectViewModel(XLObject xlobj, XLObjectViewModel parent)
        {
            _xlobject = xlobj;
            Parent = parent;

            Children = new ReadOnlyCollection<XLObjectViewModel>(
                    (from child in xlobj.Children
                     select new XLObjectViewModel(child, this))
                     .ToList());
        }

        #endregion //Constructor

        #region Presentation Members

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && Parent != null)
                    Parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region NameContainsText

        public bool NameContainsText(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(this.Name))
                return false;

            return this.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1;
        }
        #endregion //NameContainsText

        #endregion //Presentation Members

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}
