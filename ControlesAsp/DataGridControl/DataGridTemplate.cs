using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlesAsp.DataGridControl
{
    public class DataGridTemplate : ITemplate
    {

        /// <summary>
        /// Tipo de elemento a asociar a la grilla
        /// </summary>
        /// <remarks>
        /// <list>Creado: Diciembre 05 de 20113 - Ing. David Pineda</list>
        /// </remarks>
        private ListItemType _tipo_item;

        /// <summary>
        /// Texto de columna para el header o footer
        /// </summary>
        /// <remarks>
        /// <list>Creado: Diciembre 05 de 20113 - Ing. David Pineda</list>
        /// </remarks>
        private string _text_item;

        /// <summary>
        /// Control que se asocia al campo de la grilla
        /// </summary>
        /// <remarks>
        /// <list>Creado: Diciembre 05 de 20113 - Ing. David Pineda</list>
        /// </remarks>
        private Control _control;

        /// <summary>
        /// Obtiene el texto del campo establecido
        /// </summary>
        public string text_item
        {
            get { return _text_item; }
        }

        public DataGridTemplate(ListItemType tipo_item, string tex_item)
        {
            this._tipo_item = tipo_item;
            this._text_item = tex_item;
        }
    
        public DataGridTemplate(ListItemType tipo_item, Control control)
        {
            this._tipo_item = tipo_item;
            this._control = control;
        }

        public DataGridTemplate(ListItemType tipo_item)
        {
            this._tipo_item = tipo_item;
        }

        public void InstantiateIn(Control container)
        {
            Literal lc = new Literal();
            switch (_tipo_item)
            {
                case ListItemType.Header:
                    container.Controls.Add(addTittleToColumn());
                    break;
                case ListItemType.Item:
                    container.Controls.Add(addItemToColumn());
                    break;
                case ListItemType.EditItem:
                    container.Controls.Add(addEditItemToColumn());
                    break;
                case ListItemType.Footer:
                    container.Controls.Add(addFooterToColumn());
                    break;
                case ListItemType.SelectedItem:
                    container.Controls.Add(addImageButton());
                    break;
            }
        }

        private Control addImageButton()
        {
            return _control;
        }

        private Control addItemToColumn()
        {
            Literal lc = new Literal();
            lc.Text = "<B>" + this.text_item + "</B>";
            return lc;
        }

        private Control addTittleToColumn()
        {
            Literal lc = new Literal();
            lc.Text = "<B>" + this.text_item + "</B>";
            return lc;
        }

        private Control addFooterToColumn()
        {
            Literal lc = new Literal();
            lc.Text = "<B>" + this.text_item + "</B>";
            return lc;
        }

        private Control addEditItemToColumn()
        {
            TextBox text_edit = new TextBox();
            text_edit.Text = "";
            return text_edit;
        }
    }
}
