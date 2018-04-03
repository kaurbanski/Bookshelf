var startIndex = 0;
var size = 30;

$('input').keypress(function (event) {
    if (event.keyCode == 13) {
        $('#search-button').click();
    }
});

$('#search-button').click(function () {
    startIndex = 0;
    size = 30;
    $('#search-book-partial').html('');
    LoadData();
});

$('#loadMoreButton').click(function () {
    LoadData();
});

function LoadData() {
    var search = $('input#search-book-input').val();

    $.ajax({
        url: '/Books/GetBooksByPhrase',
        contentType: 'application/html',
        type: 'GET',
        dataType: 'html',
        data: {
            phrase: search,
            startIndex: startIndex,
            size: size
        },
        success: function (data) {
            if (!$.trim(data)) {
                $('#loadMoreButton').hide();
            } else {
                startIndex = startIndex + size;
                $('#search-book-partial').append(data);
                $('#loadMoreButton').show();
            }
        }
    })
}