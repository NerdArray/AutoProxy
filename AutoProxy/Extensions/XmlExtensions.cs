using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AutoProxy.Extensions
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Removes namespaces from elements in an XDocument
        /// </summary>
        /// <param name="document"><see cref="System.Xml.Linq.XDocument"/></param>
        public static void StripNamespaces(this XDocument document)
        {
            // Empty XDocument, nothing to do.
            if (document.Root == null) return;

            // Loop through all elements and remove the namespace.
            foreach (var element in document.Root.DescendantsAndSelf())
            {
                element.Name = element.Name.LocalName;

                // Remove attribute namespaces too.
                element.ReplaceAttributes(GetAttributesWithoutNamespace(element));
            }
        }

        /// <summary>
        /// Returns an IEnumerable of all XAttributes for an XElement without
        /// their namespaces.
        /// </summary>
        /// <param name="element"><see cref="System.Xml.Linq.XElement"/></param>
        /// <returns></returns>
        static IEnumerable<XAttribute> GetAttributesWithoutNamespace(XElement element)
        {
            return element.Attributes() // return all elements.
                .Where(x => !x.IsNamespaceDeclaration) // don't include namespace delcarations.
                .Select(x => new XAttribute(x.Name.LocalName, x.Value)); // remove namespaces.
        }
    }
}
