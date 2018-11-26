using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp.Basics
{
    public class LookupItem
    {   
        public string Name { get; set; }
        public int? ExternalSystemId { get; set; }
        public int? ExternalSystemParentId { get; set; }
        public virtual ICollection<LookupItem> Children { get; set; }
        public string DisplayFull(int level = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetLookupToString());
            if (Children != null && Children.Count() > 0)
            {
                level++;
                var spaces = new string(' ', level * 10);
                foreach (var child in Children)
                {
                    sb.Append($"\n{spaces} " + child.DisplayFull(level));
                }
            }

            return sb.ToString();
        }
        private string GetLookupToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Ext-Id: {ExternalSystemId} ");
            sb.Append($"Ext-ParentId: {ExternalSystemParentId} ");
            sb.Append($"Name: {Name}");
            return sb.ToString();
        }
    }
    public class FlatToHierarchiDemo
    {
        public FlatToHierarchiDemo()
        {
            var list = GetData();
            Console.WriteLine(list.ToTree2().Display());

        }
        public ICollection<LookupItem> GetData()
        {
            var list = new List<LookupItem>
            {
                new LookupItem { Name = "One-P", ExternalSystemId = 1, ExternalSystemParentId = null  },
                new LookupItem { Name = "Two-P", ExternalSystemId = 2, ExternalSystemParentId = null  },
                new LookupItem { Name = "Three-P", ExternalSystemId = 3, ExternalSystemParentId = null  },
                new LookupItem { Name = "Four-P", ExternalSystemId = 16, ExternalSystemParentId = 70  },

                new LookupItem { Name = "One-P-C", ExternalSystemId = 4, ExternalSystemParentId = 1  },
                new LookupItem { Name = "One-P-C", ExternalSystemId = 5, ExternalSystemParentId = 1  },
                new LookupItem { Name = "One-P-C", ExternalSystemId = 6, ExternalSystemParentId = 1  },

                new LookupItem { Name = "One-P-C-C", ExternalSystemId = 7, ExternalSystemParentId = 4  },
                new LookupItem { Name = "One-P-C-C", ExternalSystemId = 8, ExternalSystemParentId = 4  },
                new LookupItem { Name = "One-P-C-C", ExternalSystemId = 9, ExternalSystemParentId = 5  },


                new LookupItem { Name = "Two-P-C", ExternalSystemId = 10, ExternalSystemParentId = 2  },
                new LookupItem { Name = "Two-P-C", ExternalSystemId = 11, ExternalSystemParentId = 2  },
                new LookupItem { Name = "Two-P-C", ExternalSystemId = 12, ExternalSystemParentId = 2  },
                                         
                new LookupItem { Name = "Two-P-C-C", ExternalSystemId = 13, ExternalSystemParentId = 10  },
                new LookupItem { Name = "Two-P-C-C", ExternalSystemId = 14, ExternalSystemParentId = 10  },
                new LookupItem { Name = "Two-P-C-C", ExternalSystemId = 15, ExternalSystemParentId = 12  },

            };

            return list;
        }
    }
    public static class ExtensionMethods
    {
        public static string Display(this IEnumerable<LookupItem> items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append("\r\n" + item.DisplayFull());
            }
            return sb.ToString();
        }
        public static ICollection<LookupItem> ToTree(this ICollection<LookupItem> items, int? parentValue = null)
        {
            var list = new List<LookupItem>();
            var parentsList = items.Where(i => i.ExternalSystemParentId == parentValue);

            foreach (var item in parentsList)
            {
                item.Children = items.ToTree(item.ExternalSystemId);
            }
            list.AddRange(parentsList);
            return list;
        }
        public static ICollection<LookupItem> ToTree2(this ICollection<LookupItem> items)
        {
            var lookup = items.ToLookup(i => i.ExternalSystemParentId);
            var parentGroup = lookup.Where(l => !items.Any(i=>i.ExternalSystemId == l.FirstOrDefault().ExternalSystemParentId));
            var parentList = new List<LookupItem>();
            parentList.AddRange(parentGroup.SelectMany(i => i.ToList()));
            foreach (var item in parentList)
            {
                item.Children = items.ToTree(item.ExternalSystemId);
            }
            return parentList;
        }
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(
        this IEnumerable<T> collection,
        Func<T, K> id_selector,
        Func<T, K> parent_id_selector,
        K root_id = default(K)) {
            foreach (var c in collection.Where(c => parent_id_selector(c).Equals(root_id))) {
                yield return new TreeItem<T> {
                    Item = c,
                    Children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(c))
                };
            }
        }
    }


    public class TreeItem<T> {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
}
