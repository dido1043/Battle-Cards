$(function () {
    $('[data-toggle="tooltip"]').tooltip({
        html: true
    })
})

$("#name").focusout(function () {
    let name = $("#name").val();

    if (name.length != 0) {
        $('#card-name').text(name);
    }
})

$("#image_url").focusout(function () {
    let imageUrl = $("#image_url").val();

    if (imageUrl.length != 0) {
        $('#card-image').attr('src', imageUrl);
    }
})

$("#keyword-input").focusout(function () {
    $('#card-keyword').text($("#keyword-input").val());
})

$("#attack-input").focusout(function () {
    let attack = $("#attack-input").val();

    if (attack.length != 0) {
        $('#card-attack').text(attack);
    }
})

$("#health-input").focusout(function () {
    let health = $("#health-input").val();

    if (health.length != 0) {
        $('#card-health').text(health);
    }
})

$("#description-input").focusout(function () {
    $('#card-name').attr('data-original-title', `Description:<br>${$("#description-input").val()}`);
})