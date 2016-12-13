using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace MyDoco
{
    public partial class BrowseDoco : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
          
            var item = DatabaseProvider.MasterDatabase.GetItem(Common.GetRootPath() +"/"+ lblContentItemName.Text);
            lblHeading.Text = item.Name;
            lblItemName.Text = item.DisplayName;
            lblTemplateName.Text = item.TemplateName;

            if (item != null)
            {
                // Get Fields directly from the Item
                //----Item Fields
                List<string> fieldNames = new List<string>();
                item.Fields.ReadAll();
                FieldCollection fieldCollection = item.Fields;
                foreach (Field field in fieldCollection)
                {
                    if (!field.Name.StartsWith("__"))
                    {
                        CreateThreeColumnTable(tblFields, field.Name, field.Type, field.Section);
                    }
                }


                //----Layout Details                   
                 CreateThreeColumnTable(tblLayout, GetLayout(item,"default").ID.ToString(), GetLayout(item, "default").Name, "Default");


                //----Presentation
                foreach (RenderingReference rendering in GetRenderingReferences(item, "default"))
                {
                    CreateThreeColumnTable(tblPresentation, rendering.RenderingItem.Name, rendering.Placeholder, rendering.RenderingItem.DataSource);
                }


                //-----Data Template
                CreateThreeColumnTable(tblDataTemplate, item.TemplateID.ToString(),item.TemplateName,item.Help?.Text);



                int iter = 0;
                //----Base Template Details
                foreach (string baseTemplate in GetBaseTemplate(item))
                {
                    iter++;
                    var baseTemplateName = DatabaseProvider.MasterDatabase.GetItem(new Sitecore.Data.ID(baseTemplate)).Name;
                    CreateThreeColumnTable(tblTemplate, iter.ToString(), baseTemplate, baseTemplateName);

                    Table tbl = new Table();
                    var literalValue = new Literal();
                    literalValue.Text = "<h4>" + baseTemplateName + "</h4>";
                    var lblBaseTemplateName = new Label();
                    lblBaseTemplateName.Text = baseTemplateName;
                    var tableHeaderRow = new TableHeaderRow();
                    var tableHeaderCell1 = new TableHeaderCell();
                    var tableHeaderCell2 = new TableHeaderCell();
                    var tableHeaderCell3 = new TableHeaderCell();
                    var labelField1 = new Label();
                    labelField1.Text = "Field Name";
                    var labelField2 = new Label();
                    labelField2.Text = "Field Type";
                    var labelField3 = new Label();
                    labelField3.Text = "Field Section";
                    tableHeaderCell1.Controls.Add(labelField1);
                    tableHeaderCell2.Controls.Add(labelField2);
                    tableHeaderCell3.Controls.Add(labelField3);
                    tableHeaderRow.Controls.Add(tableHeaderCell1);
                    tableHeaderRow.Controls.Add(tableHeaderCell2);
                    tableHeaderRow.Controls.Add(tableHeaderCell3);
                    tbl.Controls.Add(tableHeaderRow);

                    tbl.CssClass = "table table-bordered";
                    foreach (var templateField in GetAllFields(baseTemplate))
                    {
                        if (!templateField.IsEmpty)
                        {
                            CreateThreeColumnTable(tbl, templateField.Name, templateField.Type, templateField.Section.Name);
                        }                    
                    }
                    plhBaseTemplateFields.Controls.Add(literalValue);
                    plhBaseTemplateFields.Controls.Add(tbl);
                }

                //-----Standard Values Rendering
                foreach (RenderingReference rendering in GetStandardValueTemplate(item, "default"))
                {
                    CreateThreeColumnTable(tblStandardValues, rendering.RenderingItem.Name, rendering.Placeholder, rendering.RenderingItem.DataSource);
                }
            }
        }
        private void CreateThreeColumnTable(Table tbl, string valueA, string valueB, string valueC)
        {
            var tableRow = new TableRow();
            tbl.Controls.Add(tableRow);
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            //Create field
            var labelField1 = new Label();
            labelField1.Text = valueA;
            var labelField2 = new Label();
            labelField2.Text = valueB;
            var labelField3 = new Label();
            labelField3.Text = valueC;
            //Add fields
            cell1.Controls.Add(labelField1);
            cell2.Controls.Add(labelField2);
            cell3.Controls.Add(labelField3);
            //Add cell to Row.
            tableRow.Controls.Add(cell1);
            tableRow.Controls.Add(cell2);
            tableRow.Controls.Add(cell3);
        }

        private void CreateFourColumnTable(Table tbl, string valueA, string valueB, string valueC, string valueD)
        {
            var tableRow = new TableRow();
            tbl.Controls.Add(tableRow);
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();
            //Create field
            var labelField1 = new Label();
            labelField1.Text = valueA;
            var labelField2 = new Label();
            labelField2.Text = valueB;
            var labelField3 = new Label();
            labelField3.Text = valueC;
            var labelField4 = new Label();
            labelField4.Text = valueC;
            //Add fields
            cell1.Controls.Add(labelField1);
            cell2.Controls.Add(labelField2);
            cell3.Controls.Add(labelField3);
            cell4.Controls.Add(labelField4);
            //Add cell to Row.
            tableRow.Controls.Add(cell1);
            tableRow.Controls.Add(cell2);
            tableRow.Controls.Add(cell3);
            tableRow.Controls.Add(cell4);
        }

        private Template GetTemplateDetails(string id)
        {
            return TemplateManager.GetTemplate(new ID(id), Sitecore.Context.Database);
        }
        private Sitecore.Data.Templates.TemplateField[] GetAllFields(string id)
        {
            Template template = TemplateManager.GetTemplate(new ID(id), DatabaseProvider.MasterDatabase);
            return template.GetFields(true).Where(p => !p.Name.StartsWith("__")).ToArray();
        }

        /// <summary>
        /// Return all placeholder keys defined on one item
        /// </summary>
        private IEnumerable<string> GetPlaceholderKeys(Sitecore.Data.Items.Item item)
        {
            List<string> uniquePlaceholderKeys = new List<string>();
            Sitecore.Layouts.RenderingReference[] renderings = GetRenderingReferences(item, "default");
            foreach (var rendering in renderings)
            {
                if (!uniquePlaceholderKeys.Contains(rendering.Placeholder))
                    uniquePlaceholderKeys.Add(rendering.Placeholder);
            }
            return uniquePlaceholderKeys;
        }

        private string[] GetBaseTemplate(Item item)
        {
            var templateItem = DatabaseProvider.MasterDatabase.GetItem(item.TemplateID);
            return templateItem["__base template"].Split('|');
        }

        private Sitecore.Layouts.RenderingReference[] GetStandardValueTemplate(Sitecore.Data.Items.Item item, string deviceName)
        {
            Sitecore.Data.Fields.LayoutField layoutField = DatabaseProvider.MasterDatabase.GetItem(item.Template.StandardValues.ID).Fields["__renderings"];
            Sitecore.Layouts.RenderingReference[] renderings = layoutField.GetReferences(GetDeviceItem(item.Database, deviceName));
            return renderings;
        }

        /// <summary>
        /// Return all renderings to be rendered in a specific placeholder on the "default" device
        /// </summary>
        private IEnumerable<Sitecore.Data.Items.RenderingItem> GetRenderings(string placeholderKey, Sitecore.Data.Items.Item item)
        {
            Sitecore.Layouts.RenderingReference[] renderings = GetRenderingReferences(item, "default");
            foreach (var rendering in renderings)
            {
                if (rendering.Placeholder == placeholderKey)
                {
                    yield return rendering.RenderingItem;
                }
            }
        }

        /// <summary>
        /// Return all renderings from an item defined on a device
        /// </summary>
        private Sitecore.Layouts.RenderingReference[] GetRenderingReferences(Sitecore.Data.Items.Item item, string deviceName)
        {
            Sitecore.Data.Fields.LayoutField layoutField = item.Fields["__renderings"];
            Sitecore.Layouts.RenderingReference[] renderings = layoutField.GetReferences(GetDeviceItem(item.Database, deviceName));
            return renderings;
        }

        /// <summary>
        /// Get the layout from an item defined on a device
        /// </summary>
        private Sitecore.Data.Items.LayoutItem GetLayout(Sitecore.Data.Items.Item item, string deviceName)
        {
            Sitecore.Data.Fields.LayoutField layoutField = item.Fields["__renderings"];
            return new Sitecore.Data.Items.LayoutItem(item.Database.GetItem(layoutField.GetLayoutID(GetDeviceItem(item.Database, deviceName))));
        }

        /// <summary>
        /// Convert a Sitecore item to a Sublayout item
        /// </summary>
        private Sitecore.Data.Items.SublayoutItem GetSublayout(Sitecore.Data.Items.Item item)
        {
            return new Sitecore.Data.Items.SublayoutItem(item);
        }

        /// <summary>
        /// Get the device item from a device name
        /// </summary>
        private Sitecore.Data.Items.DeviceItem GetDeviceItem(Sitecore.Data.Database db, string deviceName)
        {
            return db.Resources.Devices.GetAll().First(d => d.Name.ToLower() == deviceName.ToLower());
        }

    }
}