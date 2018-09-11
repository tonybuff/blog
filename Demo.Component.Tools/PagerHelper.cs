﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Demo.Component.Tools
{
    public class PagerHelper
    {
        private bool firstPageVisible;
        private int index;
        private bool isAjax;
        private string jsClass;
        private bool lastPageVisible;
        private bool nextPageVisible;
        private int numButton;
        private bool numButtonVisible;
        private string numLableFormat;
        private bool numLableVisible;

        private bool prevPageVisible;

        private int size;

        private int total;

        private string url;


        private PagerHelper(int index, int size, int total, string jsClass, string url, string numLableFormat, int? numButton, bool? numLableVisible, bool? numButtonVisible, bool? firstPageVisible, bool? prevPageVisible, bool? nextPageVisible, bool? lastPageVisible, bool isAjax)
        {
            this.numButton = 5;
            this.numLableVisible = false;
            this.numButtonVisible = true;
            this.firstPageVisible = true;
            this.prevPageVisible = true;
            this.nextPageVisible = true;
            this.lastPageVisible = true;
            this.isAjax = true;
            this.index = index;
            this.size = size;
            this.total = total;
            this.jsClass = jsClass;
            this.url = url;
            this.numLableFormat = numLableFormat;
            if (numButton.HasValue)
            {
                this.numButton = numButton.Value;
            }
            if (numLableVisible.HasValue)
            {
                this.numLableVisible = numLableVisible.Value;
            }
            if (numButtonVisible.HasValue)
            {
                this.numButtonVisible = numButtonVisible.Value;
            }
            if (firstPageVisible.HasValue)
            {
                this.firstPageVisible = firstPageVisible.Value;
            }
            if (prevPageVisible.HasValue)
            {
                this.prevPageVisible = prevPageVisible.Value;
            }
            if (nextPageVisible.HasValue)
            {
                this.nextPageVisible = nextPageVisible.Value;
            }
            if (lastPageVisible.HasValue)
            {
                this.lastPageVisible = lastPageVisible.Value;
            }
            this.isAjax = isAjax;
            if (!isAjax)
            {
                this.InitUrl();
            }
        }

        /// <summary>
        /// 采用ajax获取数据
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">显示条数</param>
        /// <param name="total">总条数</param>
        /// <param name="jsClass">js翻页代码（index.page）</param>
        /// <param name="numLableFormat"></param>
        /// <param name="numButton"></param>
        /// <param name="numLableVisible"></param>
        /// <param name="numButtonVisible"></param>
        /// <param name="firstPageVisible"></param>
        /// <param name="prevPageVisible"></param>
        /// <param name="nextPageVisible"></param>
        /// <param name="lastPageVisible"></param>
        /// <returns></returns>
        public static string CreatePagerByAjax(int index, int size, int total, string jsClass, string numLableFormat, int numButton, bool numLableVisible, bool numButtonVisible, bool firstPageVisible, bool prevPageVisible, bool nextPageVisible, bool lastPageVisible)
        {
            PagerHelper p = new PagerHelper(index, size, total, jsClass, null, numLableFormat, new int?(numButton), new bool?(numLableVisible), new bool?(numButtonVisible), new bool?(firstPageVisible), new bool?(prevPageVisible), new bool?(nextPageVisible), new bool?(lastPageVisible), true);
            return p.GetHtml();
        }

        public static string CreatePagerByUrl(int index, int size, int total, string url)
        {
            PagerHelper p = new PagerHelper(index, size, total, null, url, null, null, null, null, null, null, null, null, false);
            return p.GetHtml();
        }

        /// <summary>
        /// 采用ajax获取数据
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">显示条数</param>
        /// <param name="total">总条数</param>
        /// <param name="jsClass">js翻页代码（index.page）</param>
        /// <param name="numLableVisible">共 {0} 条  第 {1} / {2} 页是否可见</param>
        /// <returns></returns>
        public static string CreatePagerByAjax(int index, int size, int total, string jsClass, bool numLableVisible)
        {
            PagerHelper p = new PagerHelper(index, size, total, jsClass, null, null, null, numLableVisible, null, null, null, null, null, true);
            return p.GetHtml();
        }

        private string GetHtml()
        {
            int pages = 1;
            if (this.total != 0)
            {
                pages = this.total / this.size;
                if ((this.total % this.size) > 0)
                {
                    pages++;
                }
            }
            if (pages == 1)
            {
                return null;
            }
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<div class=\"ucpage clearfix\">");
            strHtml.Append("<input name=\"hid-com-page-pages\" type=\"hidden\" value=\"" + pages + "\" />");
            strHtml.Append("<input name=\"hid-com-page-index\" type=\"hidden\" value=\"" + this.index + "\" />");
            if (this.numLableVisible)
            {
                string strFormat = "共 {0} 条  第 {1} / {2} 页";
                if (!string.IsNullOrWhiteSpace(this.numLableFormat))
                {
                    strFormat = this.numLableFormat;
                }
                strHtml.Append("<div class=\"uc_page_count\">");
                strHtml.AppendFormat(strFormat, "<b>" + this.total + "</b>", "<b>" + this.index + "</b>", "<b>" + pages + "</b>");
                strHtml.Append("</div>");
            }
            strHtml.Append("<div class=\"uc_page\">");
            if (this.prevPageVisible && (this.index > 1))
            {
                if (this.isAjax)
                {
                    strHtml.AppendFormat("<a href=\"javascript:;\" onclick=\"{0}({1}); return false;\"><span>上一页</span></a>", this.jsClass, this.index - 1);
                }
                else
                {
                    strHtml.AppendFormat("<a href=\"{0}page={1}\"><span>上一页</span></a>", this.url, this.index - 1);
                }
            }
            if (this.numButtonVisible)
            {
                int startPage = 1;
                int endPage = 1;
                if (pages > this.numButton)
                {
                    if ((this.index - (this.numButton / 2)) > 0)
                    {
                        if ((this.index + (this.numButton / 2)) < pages)
                        {
                            startPage = this.index - (this.numButton / 2);
                            endPage = (startPage + this.numButton) - 1;
                        }
                        else
                        {
                            endPage = pages;
                            startPage = (endPage - this.numButton) + 1;
                        }
                    }
                    else
                    {
                        endPage = this.numButton;
                    }
                }
                else
                {
                    startPage = 1;
                    endPage = pages;
                }
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i != this.index)
                    {
                        if (this.isAjax)
                        {
                            strHtml.AppendFormat("<a href=\"javascript:;\" onclick=\"{0}({1}); return false;\"><span>{1}</span></a>", this.jsClass, i);
                        }
                        else
                        {
                            strHtml.AppendFormat("<a href=\"{0}page={1}\"><span>{1}</span></a>", this.url, i);
                        }
                    }
                    else
                    {
                        strHtml.AppendFormat("<i><span>{0}</span></i>", i);
                    }
                }
            }
            if (this.nextPageVisible && (this.index < pages))
            {
                if (this.isAjax)
                {
                    strHtml.AppendFormat("<a href=\"javascript:;\" onclick=\"{0}({1}); return false;\"><span>下一页</span></a>", this.jsClass, this.index + 1);
                }
                else
                {
                    strHtml.AppendFormat("<a href=\"{0}page={1}\"><span>下一页</span></a>", this.url, this.index + 1);
                }
            }
            if (this.lastPageVisible)
            {
                if (this.index < pages)
                {
                    if (this.isAjax)
                    {
                        strHtml.AppendFormat("<a href=\"javascript:;\" onclick=\"{0}({1}); return false;\"><span>末页</span></a>", this.jsClass, pages);
                    }
                    else
                    {
                        strHtml.AppendFormat("<a href=\"{0}page={1}\"><span>末页</span></a>", this.url, pages);
                    }
                }
                else
                {
                    strHtml.Append("<i><span>末页</span></i>");
                }
            }
            strHtml.Append("</div>");
            strHtml.Append("</div>");
            return strHtml.ToString();
        }




        private void InitUrl()
        {
            this.url = HttpContext.Current.Request.Path;
            NameValueCollection coll = HttpContext.Current.Request.QueryString;
            StringBuilder sb = new StringBuilder();
            if (coll.Count > 0)
            {
                string[] requestarr = coll.AllKeys;
                foreach (string p in requestarr)
                {
                    if (!p.EndsWith("page", true, null) && !p.EndsWith("t", true, null))
                    {
                        sb.Append(p + "=" + HttpUtility.UrlEncode(HttpContext.Current.Request.QueryString[p]) + "&");
                    }
                }
            }
            this.url = this.url + "?" + sb.ToString();
        }





    }
}
