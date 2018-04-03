$(document).ready(function () {
    loadDatatables();
});

// Load and render datatable, data fetch by ajax
function loadDatatables() {
    $('#user-books-datatable').DataTable({
        "processing": true,
        "deferRender": false,
        "ajax": "/Books/GetUserBookshelf",
        "columns": [
            {
                "data": "Id",
            },
            {
                "data": "Book.ImageLink",
                "render": function (data, type, row) {
                    return '<img src="' + data + '" class="img-responsive"/>';
                }
            },
            {
                "data": "Book.Title",
                "render": function (data, type, row) {
                    return ('<a href="book-details/' + row['Book']['GoogleId'] + '">' + data + '</a>');
                }
            },
            {
                "data": "Book.Authors",
            },
            {
                "data": "AddedDate",
                "render": function (data, type, row) {
                    var parsedDate = parseDate(data);
                    return parsedDate;
                }
            },
            {
                "data": "BeenRead",
                "render": function (data, type, row) {
                    if (data == true) {
                        return '<button class="btn btn-success btn-sm" value="' + row['Id'] + '">READ</button>';
                    } else {
                        return '<button class="btn btn-danger btn-sm" value="' + row['Id'] + '">UNREAD</button>'
                    }
                }
            },
            {
                "data": "Id",
                "render": function (data, type, row) {
                    return '<span class="glyphicon glyphicon-trash" value="' + row['Id'] + '"></span>'
                }
            }

        ],
        "columnDefs": [
            {
                "targets": [0, 1, 6],
                "orderable": false,
                "searchable": false
            },
            {
                "targets": [0],
                "visible": false
            }
        ],
        "order": [[2, "asc"]],
        "initComplete": function (settings, json) {
            prepareButtonsForChangeReadingOfBook();
            prepareRemoveButtons();
        }
    });
}

function parseDate(data) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(data);
    var dt = new Date(parseFloat(results[1]));
    return ((("0" + dt.getDate()).slice(-2)) + "-" + (("0" + (dt.getMonth() + 1)).slice(-2)) + "-" + dt.getFullYear());
}

function prepareButtonsForChangeReadingOfBook() {
    var dataTable = $('#user-books-datatable').DataTable();
    var buttons = dataTable.$('button.btn');
    $.each(buttons, function () {
        $(this).click(function () {
            var id = $(this).val();
            var button = $(this);
            var jsonObject = {
                "shelfId": id
            };
            $.ajax({
                type: 'POST',
                url: '/Books/ChangeTheReadingOfTheBook',
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
        });
    });
}

function prepareRemoveButtons() {
    $("#user-books-datatable tbody").on("click", "span.glyphicon.glyphicon-trash", function () {
        var dataTable = $('#user-books-datatable').DataTable();
        var $par = $(this).parent();
        var tbody = $(this);
        var id = dataTable.cell($par).data();
        var jsonObject = {
            "id": id
        };

        $.ajax({
            type: 'POST',
            url: '/Books/DeleteShelf',
            data: JSON.stringify(jsonObject),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result == "success") {
                    dataTable.row(tbody.parents('tr')).remove().draw();
                } else {
                    alert("Something went wrong!");
                }
            },
            error: function (response) {
                alert("Something went wrong!");
            }
        })

    });
}

