using System.Web.UI;

namespace MyDoco
{
    public class CategoryDataSource : HierarchicalDataSourceControl, IHierarchicalDataSource {
		public CategoryDataSource() : base() { }

		// Return a strongly typed view for the current data source control.
		private CategoryDataSourceView view = null;
		protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath) {
			if (null == view) {
				view = new CategoryDataSourceView(viewPath);
			}
			return view;
		}

		// This can be used declaratively. To enable declarative use, 
		// override the default implementation of CreateControlCollection 
		// to return a ControlCollection that you can add to.
		protected override ControlCollection CreateControlCollection() {
			return new ControlCollection(this);
		}
	}


	public class CategoryDataSourceView : HierarchicalDataSourceView {

		private string _viewPath;
		public CategoryDataSourceView(string viewPath) {
			_viewPath = viewPath;
		}

		public override IHierarchicalEnumerable Select() {
			CategoryCollection collection = new CategoryCollection();
            var item = Common.GetCurrentItem();
            foreach (Category category in Common.GetCategoryData()) {
				if (category.ParentId == item.ID.ToString())
					collection.Add(category);
			}
			return collection;
		}

	}

}