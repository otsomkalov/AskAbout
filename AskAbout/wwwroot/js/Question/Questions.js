let like = (id) => {
    $.ajax({
        url: '/Question/Like/' + id,
        type:'POST'
    })
}