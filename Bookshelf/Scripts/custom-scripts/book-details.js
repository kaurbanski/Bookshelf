var replacePlace = $('span#add-book-icon');

function LoadBookDetailsPage(bookJSON) {
    IsBookAddedToBookshelf(bookJSON.GoogleId);
    LoadComments(bookJSON.GoogleId);
}

$('#add-book-icon').click(function () {
    AddBookAjax(jsonObject);
});

function AddBookAjax(jsonObject) {
    $.ajax({
        type: 'POST',
        url: '/Books/AddBookshelf',
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("added succes");
            replacePlace.removeClass();
            replacePlace.addClass("glyphicon glyphicon-folder-open");
        }
    });
}

function IsBookAddedToBookshelf(bookId) {
    $.ajax({
        type: 'GET',
        url: '/Books/IsBookshelfInDatabase',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: 'googleBookId=' + bookId,
        success: function (response) {
            if (response.IsAdded == true) {
                replacePlace.removeClass();
                replacePlace.addClass("glyphicon glyphicon-folder-open");
                $('.leave-comment').removeClass('hide');
            } else {
                replacePlace.removeClass();
                replacePlace.addClass("glyphicon glyphicon-plus");
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function LoadComments(bookId) {

    $.ajax({
        type: 'GET',
        url: '/Comments/GetCommentsForBook',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: 'bookId=' + bookId,
        success: function (response) {
            $('#comments-section').html(response);

            $('button').click(function () {
                AddComment(bookId);
            });
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function AddComment(bookId) {

    var loader = $('.loader-section');
    loader.addClass('loader');
    var text = $('textarea').val();

    var jsonObject = {
        "GoogleBookId": bookId,
        "Text": text
    };

    $.ajax({
        type: 'POST',
        url: '/Comments/Add',
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('textarea').val("");
            var date = formatJSONDate(response.AddedDate);
            console.log(date);
            $('div.loader-section').after(
                $("<div class='col-md-12'>" +
                "<div class='comment-block'>" +
                    "<div class='panel panel-default'>" +
                        "<div class='comment-date'>" +
                           date +
                            "</div>" +
                        "<p class='comment-text'>" +
                            response.Text +
                        "</p>" +
                    "</div>" +
                "</div>" +
            "</div>").hide().fadeIn(1000)
           );
            loader.removeClass('loader');

            $('.comment-success').fadeIn(500);
            $('.comment-success').delay(1000).fadeOut(500);
        },
        error: function (response) {
            alert("Something went wrong!");
        }
    })
}

function formatJSONDate(jsonDate) {
    var date = new Date(parseInt(jsonDate.substr(6)));
    var formatted =
        ("0" + date.getDate()).slice(-2) + "." +
        ("0" + (date.getMonth() + 1)).slice(-2) + "." +
        date.getFullYear() + " " +
        ("0" + date.getHours()).slice(-2) + ":" +
        ("0" + date.getMinutes()).slice(-2) + ":" +
        ("0" + date.getSeconds()).slice(-2);
    return formatted;
}