"use strict";

class Like {
    static like(el, id, route)
    {
        $.get(window.location.origin + route + id, (data, status) => {
            if (status === "success") {
                let newDislikeButton = el.nextElementSibling;
                if (!newDislikeButton.classList.contains('outline')) {
                    newDislikeButton.innerText = parseInt(newDislikeButton.innerText) - 1;
                }
                newDislikeButton.classList = "thumbs outline down icon"
                newDislikeButton.setAttribute('onclick', 'dislike(this,' + id + ')');
                let newLikeButton = document.createElement('i');
                newLikeButton.classList += "thumbs up icon";
                newLikeButton.setAttribute('onclick', 'removeLike(this,' + id + ')');
                newLikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newLikeButton);
            }
        });
    }

    static dislike(el, id, route)
    {
        $.get(window.location.origin + route + id, (data, status) => {
            if (status === "success") {
                let newLikeButton = el.previousElementSibling;
                if (!newLikeButton.classList.contains('outline')) {
                    newLikeButton.innerText = parseInt(newLikeButton.innerText) - 1;
                }
                newLikeButton.classList = "thumbs outline up icon"
                newLikeButton.setAttribute('onclick', 'like(this,' + id + ')');
                let newDislikeButton = document.createElement('i');
                newDislikeButton.classList += "thumbs down icon";
                newDislikeButton.setAttribute('onclick', 'removeDislike(this,' + id + ')');
                newDislikeButton.innerText = parseInt(el.innerText) + 1;
                el.replaceWith(newDislikeButton);
            }
        });
    }

    static removeLike(el, id, route)
    {
        $.get(window.location.origin + route + id, (data, status) => {
            if (status === "success") {
                let newLikeButton = document.createElement('i');
                newLikeButton.classList += "thumbs outline up icon";
                newLikeButton.setAttribute('onclick', 'like(this,' + id + ')');
                newLikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newLikeButton);
            }
        });
    }

    static removeDislike(el, id, route)
    {
        $.get(window.location.origin + route + id, (data, status) => {
            if (status === "success") {
                let newDislikeButton = document.createElement('i');
                newDislikeButton.classList += "thumbs outline down icon";
                newDislikeButton.setAttribute('onclick', 'dislike(this,' + id + ')');
                newDislikeButton.innerText = parseInt(el.innerText) - 1;
                el.replaceWith(newDislikeButton);
            }
        });
    }
}