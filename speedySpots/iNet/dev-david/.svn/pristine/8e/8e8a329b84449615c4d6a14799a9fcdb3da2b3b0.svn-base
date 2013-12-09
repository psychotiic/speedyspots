function gradient(id, level) {
    var box = document.getElementById(id);
    box.style.opacity = level;
    box.style.MozOpacity = level;
    box.style.KhtmlOpacity = level;
    box.style.filter = "alpha(opacity=" + level * 100 + ")";
    box.style.display = "block";
    return;
}


function fadein(id) {
    var level = 0;
    while (level <= 1) {
        setTimeout("gradient('" + id + "'," + level + ")", (level * 1000) + 10);
        level += 0.01;
    }
}


// Open the lightbox


function openbox(formtitle, fadin, boxid) {
    var box = document.getElementById(boxid);
    document.getElementById('filter').style.display = 'block';

    var btitle = document.getElementById('boxtitle');
    btitle.innerHTML = formtitle;

    if (fadin) {
        gradient(boxid, 0);
        fadein(boxid);
    }
    else {
        box.style.display = 'block';
    }
}


// Close the lightbox

function closebox(boxid) {
    document.getElementById(boxid).style.display = 'none';
    document.getElementById('filter').style.display = 'none';
}