$(document).ready(function () {
    $('.next').click(function () {
        var data = {
            gameId: $('.game').data('gameId'),
            score: $('#score_input').val()
        };
        $('#score_input').val('');
        $('#score_input').focus();
        $.ajax({
            type: 'POST',
            url: '/Game/SetScore',
            data: data,
            success: function (result) {
                $('.score').text(result.Score);
                $('.number_darts').text(result.NumberOfDartsThrown);
                if (result.IsFinished) {
                    $('.score_container').remove();
                }
            },
            error: function (result) {
                
            }
        });
    });
});

