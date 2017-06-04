"use strict";

let like = (el, id) => {
    Like.like(el, id, "/Questions/Like/");
}

let dislike = (el, id) => {
    Like.dislike(el, id, "/Questions/Dislike/");
}

let removeLike = (el, id) => {
    Like.removeLike(el, id, "/Questions/ResetLike/");
}

let removeDislike = (el, id) => {
    Like.removeDislike(el, id, "/Questions/ResetLike/");
}