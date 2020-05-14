//<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
//    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.11.2/css/all.css">
//        <link href="~/Downloads/css/simplePagination.css" rel="stylesheet" />
//        <link href="~/User/css/Styleuser.css" rel="stylesheet" />
//        <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">



//<script src="https://code.jquery.com/jquery-3.4.1.js"></script>
//    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
//    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
//    <script src="https://cdnjs.cloudflare.com/ajax/libs/twbs-pagination/1.4.1/jquery.twbsPagination.min.js"></script>
//    <script src="~/Downloads/tumlum.js"></script>



$("#keyword").autocomplete({
        minLength: 3,
        source: '/product/getjson',
        select: function (event, ui) {
            window.location = '/product/details/' + ui.item.id;
            return false;
        }
    }).autocomplete("instance")._renderItem = function (ul, item) {
        var s = '';
        s += '<div class="row">';
        s += '<div class="col-sm-4">';
        s += '<img src="/User/image/' + item.photo + '" width="70" height="50" />';
        s += '</div>';
        s += '<div class="col-sm-8">';
        s += '<div><b>' + item.name + '</b></div>';
        s += '<div style="color:red">' + item.price + ' đ</div>';
        s += '</div>';
        s += '</div>';
        return $("<li>").html(s).appendTo(ul);
    };

$('#keyword').keyup(function (e) {
    var text = e.key.toLowerCase();
    if (text == "enter") {
        console.log($(this).val());
        $('#log').hide();
        window.location = '/product/search/?text=' + $(this).val();
    }
})

$(".getUrl").click(function () {
    setCookie("currentUrl", window.location.href, 1);
})

function GetPageCurrent() {
    var pathlocation = window.location.pathname;
    var page = pathlocation.split('/');
    if (page[1] == 'home') {
        if (page[2] == 'page') {
            return page[3];
        } else if (page[2] == 'categorypage') {
            return page[4];
        }
    } else {
        return 1;
    }
}

function phantrang(baseElement, totalPages, url) {
    $(baseElement).pagination({
        items: totalPages,
        itemsOnPage: 9,
        currentPage: GetPageCurrent(),
        cssStyle: 'light-theme',
        hrefTextPrefix: url,
        hrefTextSuffix: '',
        onPageClick(pageNumber, event) {
        },
    });
}



function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}