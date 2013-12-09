function sWOBlurred(e){
var sWOt;if(!e)var e=window.event;
if(e.target){sWOt=e.target} else if(e.srcElement){sWOt=e.srcElement}
if(sWOt.nodeType==3){sWOt=target.parentNode}
if(sWOt.value.length>0){sWOSend(escape(sWOt.form.name),escape(sWOt.name),escape(sWOt.value));}}
function sWOSend(pFr,pFi,pFv){
var iWO=new Image(1,1);
var stPUrl=sWOProtocol+"//"+sWOGateway+"/form.gif?u="+sWOSession+"&d="+sWODomain+"&frm='"+pFr+"'&frn='"+pFi+"'&frd='"+pFv+"'";iWO.src=stPUrl;}
var sWz=document.getElementsByTagName("input");
for (idx=0;idx<sWz.length;idx++){if(sWz[idx].type.indexOf("text")>-1||sWz[idx].type.indexOf("checkbox")>-1||sWz[idx].type.indexOf("radio")>-1){sWz[idx].onblur=sWOBlurred;}}
var sWx=document.getElementsByTagName("select");
for (idx=0;idx<sWx.length;idx++){sWx[idx].onblur=sWOBlurred;}
var sWy=document.getElementsByTagName("form");
for (idx=0;idx<sWy.length;idx++){if(!sWy[idx].name||sWy[idx].name==""){sWy[idx].name="form"+idx;}}