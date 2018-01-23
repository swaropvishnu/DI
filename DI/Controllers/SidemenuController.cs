using DI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace DI.Controllers
{
    public class SidemenuController : Controller
    {
        // GET: Sidemenu
        [ChildActionOnly]
        public void Index()
        {
            //return View();
            return ExecuteXSLTransformation();
        }
        public string ExecuteXSLTransformation()
        {
            string HtmlTags, XsltPath;
            MemoryStream DataStream = default(MemoryStream);
            StreamReader streamReader = default(StreamReader);

            try
            {
                //Path of XSLT file
                XsltPath = HttpContext.Current.Server.MapPath("~/App_Data/XSLTFile.xslt");
                //Encode all Xml format string to bytes
                byte[] bytes = Encoding.Unicode.GetBytes(GenerateXmlFormat());
                DataStream = new MemoryStream(bytes);
                //Create Xmlreader from memory stream
                XmlReader reader = XmlReader.Create(DataStream);
                // Load the XML
                XPathDocument document = new XPathDocument(reader);
                XslCompiledTransform XsltFormat = new XslCompiledTransform();
                // Load the style sheet.
                XsltFormat.Load(XsltPath);
                DataStream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(DataStream, Encoding.Unicode);
                //Apply transformation from xml format to html format and save it in xmltextwriter
                XsltFormat.Transform(document, writer);
                streamReader = new StreamReader(DataStream);
                DataStream.Position = 0;
                HtmlTags = streamReader.ReadToEnd();
                //divMenus.InnerHtml = HtmlTags;
                return HtmlTags;
            }
            catch (Exception ex)
            {

                //return ex.Message;
            }
            finally
            {
                //Release the resources

                //streamReader.Close();

                //DataStream.Close();
            }

        }

        public string GenerateXmlFormat()
        {

            DataSet DbMenu = new DataSet();
            DbMenu = UserDtl.GetMenuData(UserSession.LoggedInUser.UserId);
            DbMenu.DataSetName = "Menus";
            DbMenu.Tables[0].TableName = "Menu";
            //create Relation Parent and Child
            DataRelation relation;
            relation = new DataRelation("ParentChild", DbMenu.Tables["Menu"].Columns["MenuID"], DbMenu.Tables["Menu"].Columns["ParentId"], true);
            relation.Nested = true;
            DbMenu.Relations.Add(relation);
            return DbMenu.GetXml();
        }
    }
}