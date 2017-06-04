"use strict";

let likeReply = (el, id) => {
    Like.like(el, id, "/Replies/Like/");
}

let dislikeReply=(el, id) => {
    Like.like(el, id, "/Replies/Dislike/");
}

let removeLikeReply = (el, id) => {
    Like.like(el, id, "/Replies/ResetLike/");
}

let removeDislikeReply = (el, id) => {
    Like.like(el, id, "/Replies/ResetLike/");
}

let likeComment = (el, id) => {
    Like.like(el, id, "/Comments/Like/");
}

let dislikeComment = (el, id) => {
    Like.like(el, id, "/Comments/Dislike/");
}

let removeLikeComment = (el, id) => {
    Like.like(el, id, "/Comments/ResetLike/");
}

let removeDislikeComment = (el, id) => {
    Like.like(el, id, "/Comments/ResetLike/");
}