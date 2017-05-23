let like = (el, id) => {
    $.get(window.location.origin + "/Likes/Like/" + id, (data, status) => {
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

let dislike = (el, id) => {
    $.get(window.location.origin + "/Likes/Dislike/" + id, (data, status) => {
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

let removeLike = (el, id) => {
    $.get(window.location.origin + "/Likes/Delete/" + id, (data, status) => {
        if (status === "success") {
            let newLikeButton = document.createElement('i');
            newLikeButton.classList += "thumbs outline up icon";
            newLikeButton.setAttribute('onclick', 'like(this,' + id + ')');
            newLikeButton.innerText = parseInt(el.innerText) - 1;
            el.replaceWith(newLikeButton);
        }
    });
}

let removeDislike = (el, id) => {
    $.get(window.location.origin + "/Likes/Delete/" + id, (data, status) => {
        if (status === "success") {
            let newDislikeButton = document.createElement('i');
            newDislikeButton.classList += "thumbs outline down icon";
            newDislikeButton.setAttribute('onclick', 'dislike(this,' + id + ')');
            newDislikeButton.innerText = parseInt(el.innerText) - 1;
            el.replaceWith(newDislikeButton);
        }
    });
}