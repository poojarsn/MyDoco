using System;
using System.Web.UI;

namespace MyDoco
{

	public class Category : IHierarchyData {

		private string _categoryId;
		private string _parentId;
		private string _name;

		/// <summary>
		/// Unique identifier for the category
		/// </summary>
		public string CategoryId {
			get { return _categoryId; }
			set { _categoryId = value; }
		}

		/// <summary>
		/// Foreign key to the parent category
		/// </summary>
		public string ParentId {
			get { return _parentId; }
			set { _parentId = value; }
		}

		/// <summary>
		/// Friendly description of the category
		/// </summary>
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Hide the default public constructor
		/// </summary>
		private Category() {
		}

		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="categoryId">Unique identifier for the category</param>
		/// <param name="parentId">Foreign key to the parent category</param>
		/// <param name="name">Friendly description of the category</param>
		public Category(string categoryId, string parentId, string name) {
			_categoryId = categoryId;
			_parentId = parentId;
			_name = name;
		}

		#region IHierarchyData Members

		public IHierarchicalEnumerable GetChildren() {

			CategoryCollection children = new CategoryCollection();

			// Loop through your local data and find any children
			foreach (Category category in Common.GetCategoryData()) {
				if (category.ParentId == this.CategoryId) {
					children.Add(category);
				}
			}

			return children;

		}

		public IHierarchyData GetParent() {

			// Loop through your local data and report back with the parent
			foreach (Category category in Common.GetCategoryData()) {
				if (category.CategoryId == this.ParentId)
					return category;
			}

			return null;

		}

		public bool HasChildren {
			get {
				CategoryCollection children = GetChildren() as CategoryCollection;
				return children.Count > 0;
			}
		}

		public object Item {
			get { return this; }
		}

		public string Path {
			get { return this.CategoryId.ToString(); }
		}

		public string Type {
			get { return this.GetType().ToString(); }
		}

		#endregion

		public override string ToString() {
			return this.Name;
		}

	}

}