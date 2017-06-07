let addReplyButton;

let replyField = (el, id) => {
    $.get(window.location.origin + "/Replies/Create/" + id,
        (data, status) => {
            if (status === "success") {
                let newEl = document.createElement('div');
                newEl.innerHTML = data;
                addReplyButton = el;
                el.replaceWith(newEl);
                $.validator.unobtrusive.parse($('#replyForm'));
            }
        });
}

let cancelReply = () => {
    $('#replyForm').replaceWith(addReplyButton);
}

let addCommentButton;

let commentField = (el, id) => {
    $.get(window.location.origin + "/Comments/Create/" + id,
        (data, status) => {
            if (status === "success") {
                let newEl = document.createElement('div');
                newEl.innerHTML = data;
                addCommentButton = el;
                el.replaceWith(newEl);
                $.validator.unobtrusive.parse($('#commentForm'));
            }
        });
}

let cancelComment = () => {
    $('#commentForm').replaceWith(addCommentButton);
}