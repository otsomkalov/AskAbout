let like = (el,id) => {
    $.get("Likes/Like/" + id, (data, status) => {
        if (status === "success") {
            let newDislikeButton = el.nextElementSibling;
            newDislikeButton.classList ="btn btn-default dislike"
            newDislikeButton.setAttribute('onclick', 'dislike(this,' + id + ')');
            let newLikeButton = document.createElement('button');
            newLikeButton.innerText = "Like";
            newLikeButton.classList += "btn btn-success like";
            newLikeButton.setAttribute('onclick', 'removeLike(this,' + id + ')');
            el.replaceWith(newLikeButton);            
        }
    });
};

//
//    <button class="btn btn-default dislike" onclick="dislike(this,@item.Id)">Dislike</button>

let dislike = (el,id) => {
    $.get("Likes/Dislike/" + id, (data, status) => {
        if (status === "success") {
            let newLikeButton = el.previousElementSibling;
            newLikeButton.classList = "btn btn-default like"
            newLikeButton.setAttribute('onclick', 'like(this,' + id + ')');
            let newDislikeButton = document.createElement('button');
            newDislikeButton.innerText = "Dislike";
            newDislikeButton.classList += "btn btn-danger dislike";
            newDislikeButton.setAttribute('onclick', 'removeDislike(this,' + id + ')');
            el.replaceWith(newDislikeButton);  
        }
    });
}

let removeLike = (el,id) => {
    $.get("Likes/Delete/" + id, (data, status) => {
        if (status === "success") {
            let newLikeButton = document.createElement('button');
            newLikeButton.innerText = "Like";
            newLikeButton.classList += "btn btn-default like";
            newLikeButton.setAttribute('onclick', 'like(this,' + id + ')');
            el.replaceWith(newLikeButton);
        }
    });
}

let removeDislike = (el, id) => {
    $.get("Likes/Delete/" + id, (data, status) => {
        if (status === "success") {
            let newDislikeButton = document.createElement('button');
            newDislikeButton.innerText = "Dislike";
            newDislikeButton.classList += "btn btn-default like";
            newDislikeButton.setAttribute('onclick', 'dislike(this,' + id + ')');
            el.replaceWith(newDislikeButton);
        }
    });
}