"use strict";

let like = (el, id) => {
    $.get(window.location.origin + "/Questions/Like/" + id,
        (data, status) => {
            if (status === "success") {
                const newDislikeButton = el.nextElementSibling;
                if (!newDislikeButton.classList.contains("outline")) {
                    newDislikeButton.innerText = parseInt(newDislikeButton.innerText) - 1;
                }
                newDislikeButton.classList = "thumbs outline down icon";
                newDislikeButton.setAttribute("onclick", `dislike(this,${id})`);
                const newLikeButton = document.createElement("i");
                newLikeButton.classList += "thumbs up icon";
                newLikeButton.setAttribute("onclick", `removeLike(this,${id})`);
                newLikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newLikeButton);
            }
        });
};
let dislike = (el, id) => {
    $.get(window.location.origin + "/Questions/Dislike/" + id,
        (data, status) => {
            if (status === "success") {
                const newLikeButton = el.previousElementSibling;
                if (!newLikeButton.classList.contains("outline")) {
                    newLikeButton.innerText = parseInt(newLikeButton.innerText) - 1;
                }
                newLikeButton.classList = "thumbs outline up icon";
                newLikeButton.setAttribute("onclick", `like(this,${id})`);
                const newDislikeButton = document.createElement("i");
                newDislikeButton.classList += "thumbs down icon";
                newDislikeButton.setAttribute("onclick", `removeDislike(this,${id})`);
                newDislikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newDislikeButton);
            }
        });
};
let removeLike = (el, id) => {
    $.get(window.location.origin + "/Questions/ResetLike/" + id,
        (data, status) => {
            if (status === "success") {
                const newLikeButton = document.createElement("i");
                newLikeButton.classList += "thumbs outline up icon";
                newLikeButton.setAttribute("onclick", `like(this,${id})`);
                newLikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newLikeButton);
            }
        });
};
let removeDislike = (el, id) => {
    $.get(window.location.origin + "/Questions/ResetLike/" + id,
        (data, status) => {
            if (status === "success") {
                const newDislikeButton = document.createElement("i");
                newDislikeButton.classList += "thumbs outline down icon";
                newDislikeButton.setAttribute("onclick", `dislike(this,${id})`);
                newDislikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newDislikeButton);
            }
        });
};