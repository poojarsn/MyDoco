using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MyDoco
{ 
	public class CategoryCollection : List<Category>, IHierarchicalEnumerable {

		public CategoryCollection()
			: base() {
		}

		#region IHierarchicalEnumerable Members

		public IHierarchyData GetHierarchyData(object enumeratedItem) {
			return enumeratedItem as IHierarchyData;
		}

		#endregion

	}

}