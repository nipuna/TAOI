using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using DomainModel.Abstract;
using System.Web.Routing;
using DomainModel.Entities;

namespace WebUI.Controllers
{

    public static class PagingHelpers1
    {
        
        public static string PagingLinks(this HtmlHelper html, int currentPage, int firstPage, int lastPage, int NoOfPagingTags, Func<int, string> pageUrl)
        {

            StringBuilder result = new StringBuilder();

            for (int i = firstPage; i <= lastPage; i++)
            {
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag

                #region namings
                    if (i == firstPage)
                    {
                        tag.Attributes.Add("id" , "firstPageNav");
                    }
                    if (i == firstPage && i > firstPage + NoOfPagingTags - 1)
                    {
                        tag.Attributes.Add("style", "visibility:hidden;");
                    }
                    if( i == firstPage + 1)
                    {
                        tag.Attributes.Add("id", "secondPageNav");
                    }

                    if (  i == firstPage + 1 && i > firstPage + NoOfPagingTags - 1)
                    {
                        tag.Attributes.Add("style", "visibility:hidden;");
                    } 
                
                    if (i == lastPage)
                    {
                        tag.Attributes.Add("id", "thirdPageNav");
                    }
                    if (i == lastPage && i > firstPage + NoOfPagingTags - 1)
                    {
                        tag.Attributes.Add("style", "visibility:hidden;");
                    }
                #endregion

                    #region namingsNew
                    //if (i == firstPage && firstPage != lastPage)
                    //{
                    //    tag.Attributes.Add("id", "firstPageNav");
                    //}
                    //if (i == firstPage && firstPage != lastPage && i > firstPage + NoOfPagingTags - 1)
                    //{
                    //    tag.Attributes.Add("style", "visibility:hidden;display: none;");
                    //}
                    //if (i == firstPage + 1 && firstPage + 1 != lastPage)
                    //{
                    //    tag.Attributes.Add("id", "secondPageNav");
                    //}

                    //if (i == firstPage + 1 && firstPage + 1 != lastPage && i > firstPage + NoOfPagingTags - 1)
                    //{
                    //    tag.Attributes.Add("style", "visibility:hidden;display: none;");
                    //}

                    //if (i == lastPage)
                    //{
                    //    tag.Attributes.Add("id", "thirdPageNav");
                    //}
                    //if (i == lastPage && i > firstPage + NoOfPagingTags - 1)
                    //{
                    //    tag.Attributes.Add("style", "visibility:hidden;display: none;");
                    //}
                    #endregion

                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == currentPage)
                {
                    tag.AddCssClass("footerbtnhov");
                }
                else
                {
                    tag.AddCssClass("footerbtn");
                }
                result.AppendLine(tag.ToString());    
                
            }
                
            return result.ToString();
                
        }
        
        /// <summary>
        /// renders the checkboxlist
        /// </summary>
        /// <param name="name"> name of the checkboxlist</param>
        /// <param name="listInfo">object with value of ID, checked status and InnerHtml </param>
        /// <param name="htmlAttributes">other style attributes </param>
        /// <returns>string containg checkboxlist tags </returns>
        public static string CheckBoxList(this HtmlHelper<DomainModel.Entities.User> htmlHelper, string name, List<details> listInfo)
        {
            return htmlHelper.CheckBoxList(name, listInfo,
                ((IDictionary<string, object>)null));
        }

        /// <summary>
        ///  renders the checkboxlist
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"> name of the checkboxlist</param>
        /// <param name="listInfo">object with value of ID, checked status and InnerHtml </param>
        /// <param name="htmlAttributes">other style attributes </param>
        /// <returns>string containg checkboxlist tags </returns>
        public static string CheckBoxList(this HtmlHelper<DomainModel.Entities.User> htmlHelper, string name, List<details> listInfo, object htmlAttributes)
        {
            return htmlHelper.CheckBoxList(name, listInfo,
                ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        /// <summary>
        /// renders the checkboxlist
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"> name of the checkboxlist</param>
        /// <param name="listInfo">object with value of ID, checked status and InnerHtml </param>
        /// <param name="htmlAttributes">other style attributes </param>
        /// <returns>string containg checkboxlist tags </returns>
        public static string CheckBoxList(this HtmlHelper<DomainModel.Entities.User> htmlHelper, string name, List<details> listInfo, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The argument must have a value", "name");
            if (listInfo == null)
                throw new ArgumentNullException("listInfo");
            if (listInfo.Count < 1)
                throw new ArgumentException("The list must contain at least one value", "listInfo");

            StringBuilder sb = new StringBuilder();
            //builder.MergeAttribute("value", info.Value);
            foreach (details info in listInfo)
            {
                string newCheckBox = "";
                if (info.Name == "All")
                {
                    newCheckBox = "<input id=\"" + name + info.ID + "\" name=\"" + name + "All" + "\" value=\"" + info.ID + "\" onclick=\"checkAllCheckBoxes(this ,'" + name + "');\" type=\"checkbox\">" + info.Name + "</input>";
                }
                else
                {
                    if (info.Status == true)
                    {
                        newCheckBox = "<input checked=\"checked\" id=\"" + name + info.ID + "\" name=\"" + name + "Choosen" + "\" value=\"" + info.ID + "\" type=\"checkbox\">" + info.Name + "</input>";
                    }
                    else
                    {
                        newCheckBox = "<input id=\"" + name + info.ID + "\" name=\"" + name + "Choosen" + "\" value=\"" + info.ID + "\" type=\"checkbox\">" + info.Name + "</input>";
                    }
                }
                sb.Append("<span id='" + name + "Span" + info.ID + "' class=\"grid_4\">");
                sb.Append(newCheckBox);
                sb.Append("</span>");
            }
            return sb.ToString();
        }

    }

}

