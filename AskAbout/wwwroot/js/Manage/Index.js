Dropzone.autoDiscover = false;

$("form").dropzone({
    url: "Index",
    clickable: true,
    parallelUploads: 1,
    maxFilesize: 2,
    paramName: "file",
    uploadMultiple: false,
    addRemoveLinks: "dictRemoveFile",
    maxFiles: 1,
    previewContainer: "#img",
    queueLimit: 1
})