﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-script.aspx.cs" Inherits="SpeedySpots.view_script" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
<title>Script</title>
<link rel="stylesheet" type="text/css" media="screen, projection" href="css/sssms-forms.css" />
<link rel="stylesheet" type="text/css" media="print" href="css/sssms-print.css" />
<style type="text/css">
body {
font-size: 100%;
font-family: "Trebuchet MS", Arial, Verdana, Helvetica, sans-serif;
}

.unavailabilities 
{
	margin: 0 5em;
}
</style>
</head>
<body>
<div class="button-row">
    <a href="javascript:window.print()" class="button primaryAction">Click to Print This Page</a>
</div>

<div id="header">
    <img src="img/ss-logo-print.gif" height="114" width="615" alt="Speedy Spots" />
</div>
<div id="body">
    <h2>Script:</h2>
    <%=IARequest.ScriptForDisplay %>
</div>
<div id="footer">
</div>
</body>
</html>