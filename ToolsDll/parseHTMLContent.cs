using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Xml;
using System.IO;

namespace ToolsDll
{
    public class parseHTMLContent
    {
        HtmlDocument hd = new HtmlDocument();
        public void loadFromFile()
        {
            HtmlWeb web = new HtmlWeb();
            TextWriter tw = new StringWriter();
            //HtmlDocument doc = web.Load(@"E:/qiaochu001/gc/www.qiaochu001.com/download/system/914.html");
            HtmlDocument doc = web.Load(@"E:/qiaochu001/gc/www.qiaochu001.com/download/system/85.html");
            Menu mu = new Menu();
            if (doc.DocumentNode.SelectSingleNode("//div[@class='xia_x']") != null)
            {
                mu.MName = doc.DocumentNode.SelectSingleNode("//div[@class='xia_x']").InnerText;
                mu.MSystem = doc.DocumentNode.SelectNodes("//div[@class='list_t']/a").Last().InnerText;

                NextType nt = NextType.Empty;
                foreach (HtmlNode hn in doc.DocumentNode.SelectNodes("//div[@class='d_left']/p"))
                {
                    if (hn.InnerText.IndexOf("做法：") >= 0 || hn.InnerText.IndexOf("烹制方法：") >= 0 || hn.InnerText.IndexOf("过程：") >= 0)
                        nt = NextType.CookMethod;
                    if (hn.InnerText.IndexOf("主料：") >= 0 || hn.InnerText.IndexOf("主料：") >= 0)
                        nt = NextType.Material;

                    if (hn.InnerText.IndexOf("辅料：") >= 0 || hn.InnerText.IndexOf("调料：") >= 0)
                        nt = NextType.SubMaterial;

                    //if(hn.SelectNodes("img")!=null)
                    //    mu.img=hn.SelectNodes("img")[0].ChildAttributes["src"]
                    if (hn.InnerText.IndexOf("知识点：") >= 0)
                        nt = NextType.ShouldKnow;
                    if (hn.InnerText.IndexOf("特点：") >= 0)
                        nt = NextType.Style;
                    if (hn.InnerText.IndexOf("提示：") >= 0)
                        nt = NextType.Tips;

                    switch (nt)
                    {
                        case NextType.CookMethod:
                            if (doc.DocumentNode.SelectNodes("//div[@id='cpInfoDown']") != null)
                                mu.nCookMethod = doc.DocumentNode.SelectNodes("//div[@id='cpInfoDown']")[0].InnerText;
                            else
                                mu.nCookMethod += hn.InnerText;
                            break;
                        case NextType.Material:
                            mu.nMaterial += hn.InnerText;
                            break;
                        case NextType.ShouldKnow:
                            mu.nShouldKnow += hn.InnerText;
                            break;
                        case NextType.SubMaterial:
                            mu.nSubMaterial += hn.InnerText;
                            break;
                        case NextType.Style:
                            mu.nStyle += hn.InnerText;
                            break;
                        case NextType.Tips:
                            mu.nTips += hn.InnerText;
                            break;
                        default:
                            break;
                    }
                }
                if (doc.DocumentNode.SelectNodes("//div[@class='d_left']/p/img") != null)
                    mu.img = doc.DocumentNode.SelectNodes("//div[@class='d_left']/p/img")[0].Attributes["src"].Value;
                else
                    if (doc.DocumentNode.SelectNodes("//p/strong/img") != null)
                        mu.img = doc.DocumentNode.SelectNodes("//p/strong/img")[0].Attributes["src"].Value;
            }
            HtmlNode hnd = doc.GetElementbyId("");
        }
    }

    public class Menu
    {
        public string img { get; set; }
        public string MName { get; set; }
        public string MSystem { get; set; }
        public string nCookMethod { get; set; }
        public string nMaterial { get; set; }
        public string nSubMaterial { get; set; }
        public string nShouldKnow { get; set; }
        public string nStyle { get; set; }
        public string nTips { get; set; }
    }

    /// <summary>
    /// 下一个节点的类型
    /// </summary>
    public enum NextType
    {
        //方法
        CookMethod, 
        //材料
        Material,
        //辅材
        SubMaterial,
        //知识点
        ShouldKnow,
        //风味
        Style,
        //注意事项/提示
        Tips, 
        //空项
        Empty
    }
}
