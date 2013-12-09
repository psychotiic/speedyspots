// Prompts the user for an Ok/Cancel response to a question/statement.
function ConfirmUser(sQuestion)
{
    return confirm(sQuestion);
}

function CleanHtml(html) {
    var styleRegex = /(<(p|span|div)\b\s+[^>]*?)(\bstyle\s*=\s*('[^']*'|"[^"]*"|[^\s>]+))/i;

    html = html.replace(/(<p[^>]*>(\s|&nbsp;?)*<\/p>[\n\s])/gi, '');
    
    var styleMatch = styleRegex.exec(html);

    while (styleMatch != null) {
        html = html.replace(styleRegex, '$1');

        styleMatch = styleRegex.exec(html);
    }

    return html;
}