var replacePlace = $('span#add-book-icon');

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

    var text = $('textarea').val();
    var jsonObject = {
        "GoogleBookId": bookId,
        "Text": text
    };
    
    console.log(jsonObject);
    $.ajax({
        type: 'POST',
        url: '/Comments/Add',
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (addedAsRead) {
            if (addedAsRead == true) {
                button.removeClass();
                button.text("READ");
                button.addClass("btn btn-success btn-sm");
            } else {
                button.removeClass();
                button.text("UNREAD");
                button.addClass("btn btn-danger btn-sm");
            }
        },
        error: function (response) {
            alert("Something went wrong!");
        }
    })
}