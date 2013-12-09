using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SpeedySpots.Services.Models
{
    public class ObjectResult<T> : ActionResult
    {
        private static UTF8Encoding                 UTF8 = new UTF8Encoding(false);

        public T Data { get; set; }

        public Type[] IncludedTypes = new[] { typeof(object) };

        public ObjectResult(T oData)
        {
            Data = oData;
        }

        public ObjectResult(T oData, Type[] oIncludedTypes)
        {
            Data = oData;
            IncludedTypes = oIncludedTypes;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if(context.HttpContext.Request.Headers["Content-Type"].Contains("application/json"))
            {
                JsonResult oResult = new JsonResult();
                oResult.Data = Data;
                oResult.ExecuteResult(context);
            }
            else
            {
                using(MemoryStream oMemoryStream = new MemoryStream(500))
                {
                    XmlWriterSettings oSettings = new XmlWriterSettings();
                    oSettings.OmitXmlDeclaration = true;
                    oSettings.Encoding = UTF8;
                    oSettings.Indent = true;

                    using(XmlWriter oXmlWriter = XmlWriter.Create(oMemoryStream, oSettings))
                    {
                        XmlSerializer oSerializer = new XmlSerializer(typeof(T), IncludedTypes);
                        oSerializer.Serialize(oXmlWriter, Data);
                    }

                    ContentResult oResult = new ContentResult();
                    oResult.ContentType = "text/xml";
                    oResult.Content = UTF8.GetString(oMemoryStream.ToArray());
                    oResult.ContentEncoding = UTF8;
                    oResult.ExecuteResult(context);
                } 
            }
        }
    }
}