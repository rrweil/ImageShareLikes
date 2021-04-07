$(() => {

    const id = $(".like").data('id');

    setInterval(function () {
        $.get('/home/GetLikes', { id }, function (likes) {
            $("#imageLikes").text(likes);
        })
    }, 1000);

    
    $(".like").on("click", function () {
        $.post('/home/addLike', { id }, function () {
        })    
    })



















    //function cb() {

    //}

    //let id = $(".like").data('id');

    //function updateLikes(cb) {
    //    $.get('/home/GetLikes', { id }, function (likes) {
    //        $("#imageLikes").text(likes)
    //        if (cb) {
    //            cb();
    //        }
    //    })
    //}

    //let getLikes = $.get('/home/GetLikes', { id }, function (likes) {
    //    $("#imageLikes").text(likes)    
    //    if (cb) {
    //        cb();
    //    }
    //})

    //setInterval(updateLikes(() => console.log("testing")), 1000);

    //setInterval($.get('/home/GetLikes', { id }, function (likes) {
    //    console.log(likes);
    //    $("#imageLikes").text(likes);

    //}), 1000);

    //this is working
    //$.get('/home/GetLikes', { id }, function (likes) {
    //    $("#imageLikes").text(likes);
    //});

    //let likes = setInterval((id) => $.get('/home/getLikes'), 1000);
    //console.log(likes);

    
})