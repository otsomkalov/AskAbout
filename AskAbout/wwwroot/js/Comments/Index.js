"use strict";

let likeComment = (el, id) => {
    $.get(window.location.origin + "/Comments/Like/" + id,
        (data, status) => {
            if (status === "success") {
                const newDislikeButton = el.nextElementSibling;
                if (!newDislikeButton.classList.contains("outline")) {
                    newDislikeButton.innerText = parseInt(newDislikeButton.innerText) - 1;
                }
                newDislikeButton.classList = "thumbs outline down icon";
                newDislikeButton.setAttribute("onclick", `dislikeComment(this,${id})`);
                const newLikeButton = document.createElement("i");
                newLikeButton.classList += "thumbs up icon";
                newLikeButton.setAttribute("onclick", `removeLikeComment(this,${id})`);
                newLikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newLikeButton);
            }
        });
};
let dislikeComment = (el, id) => {
    $.get(window.location.origin + "/Comments/Dislike/" + id,
        (data, status) => {
            if (status === "success") {
                const newLikeButton = el.previousElementSibling;
                if (!newLikeButton.classList.contains("outline")) {
                    newLikeButton.innerText = parseInt(newLikeButton.innerText) - 1;
                }
                newLikeButton.classList = "thumbs outline up icon";
                newLikeButton.setAttribute("onclick", `likeComment(this,${id})`);
                const newDislikeButton = document.createElement("i");
                newDislikeButton.classList += "thumbs down icon";
                newDislikeButton.setAttribute("onclick", `removeDislikeComment(this,${id})`);
                newDislikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newDislikeButton);
            }
        });
};
let removeLikeComment = (el, id) => {
    $.get(window.location.origin + "/Comments/ResetLike/" + id,
        (data, status) => {
            if (status === "success") {
                const newLikeButton = document.createElement("i");
                newLikeButton.classList += "thumbs outline up icon";
                newLikeButton.setAttribute("onclick", `likeComment(this,${id})`);
                newLikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newLikeButton);
            }
        });
};
let removeDislikeComment = (el, id) => {
    $.get(window.location.origin + "/Comments/ResetLike/" + id,
        (data, status) => {
            if (status === "success") {
                const newDislikeButton = document.createElement("i");
                newDislikeButton.classList += "thumbs outline down icon";
                newDislikeButton.setAttribute("onclick", `dislikeComment(this,${id})`);
                newDislikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newDislikeButton);
            }
        });
};