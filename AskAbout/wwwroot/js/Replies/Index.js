"use strict";

let likeReply = (el, id) => {
    $.get(window.location.origin + "/Replies/Like/" + id,
        (data, status) => {
            if (status === "success") {
                const newDislikeButton = el.nextElementSibling;
                if (!newDislikeButton.classList.contains("outline")) {
                    newDislikeButton.innerText = parseInt(newDislikeButton.innerText) - 1;
                }
                newDislikeButton.classList = "thumbs outline down icon";
                newDislikeButton.setAttribute("onclick", `dislikeReply(this,${id})`);
                const newLikeButton = document.createElement("i");
                newLikeButton.classList += "thumbs up icon";
                newLikeButton.setAttribute("onclick", `removeLikeReply(this,${id})`);
                newLikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newLikeButton);
            }
        });
};
let dislikeReply = (el, id) => {
    $.get(window.location.origin + "/Replies/Dislike/" + id,
        (data, status) => {
            if (status === "success") {
                const newLikeButton = el.previousElementSibling;
                if (!newLikeButton.classList.contains("outline")) {
                    newLikeButton.innerText = parseInt(newLikeButton.innerText) - 1;
                }
                newLikeButton.classList = "thumbs outline up icon";
                newLikeButton.setAttribute("onclick", `likeReply(this,${id})`);
                const newDislikeButton = document.createElement("i");
                newDislikeButton.classList += "thumbs down icon";
                newDislikeButton.setAttribute("onclick", `removeDislikeReply(this,${id})`);
                newDislikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newDislikeButton);
            }
        });
};
let removeLikeReply = (el, id) => {
    $.get(window.location.origin + "/Replies/ResetLike/" + id,
        (data, status) => {
            if (status === "success") {
                const newLikeButton = document.createElement("i");
                newLikeButton.classList += "thumbs outline up icon";
                newLikeButton.setAttribute("onclick", `likeReply(this,${id})`);
                newLikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newLikeButton);
            }
        });
};
let removeDislikeReply = (el, id) => {
    $.get(window.location.origin + "/Replies/ResetLike/" + id,
        (data, status) => {
            if (status === "success") {
                const newDislikeButton = document.createElement("i");
                newDislikeButton.classList += "thumbs outline down icon";
                newDislikeButton.setAttribute("onclick", `dislikeReply(this,${id})`);
                newDislikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newDislikeButton);
            }
        });
};