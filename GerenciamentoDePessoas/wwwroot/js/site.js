$(document).ready(function () {
    $('#buscarTotalPessoas').click(function () {
        $('#resultado').text('');

        $.ajax({
            method: 'GET',
            url: '/Pessoa/Total',
            dataType: "text",
            success: function (data) {
                $('#resultado').text(`Total de Pessoas ${data}`);
            },
            error: function (xhr, status, error) {
                Console.error(`Erro: ${status} - ${error}`);
                $('#result').text(`Erro ao buscar total de pessoas.`);
            }
        });
    });

    $('#botaoBusca').click(function () {
        var termo = $('#termoBusca').val();
        if (!termo) {
            alert('Por favor, insira um termo de pesquisa.');
            return;
        }

        $.ajax({
            url: '/Pessoa/BuscarPessoaTermo',
            type: 'GET',
            data: { termo: termo },
            success: function (data) {
                $('#resultadoPessoa').empty();
                if (data.length === 0) {
                    $('#resultadoPessoa').append('<li class="list-group-item">Nenhuma pessoa encontrada.</li>');
                } else {
                    data.forEach(function (nomeCompleto) {
                        $('#resultadoPessoa').append(`<li class="list-group-item">${nomeCompleto}</li>`);
                    });
                }
            },
            error: function (xhr, status, error) {
                console.error(`Erro: ${status} - ${error}`);
                $('#resultadoPessoa').append('<li class="list-group-item">Erro ao buscar pessoas.</li>');
            }
        });
    });

    $('.tabela-pessoas').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/2.3.2/i18n/pt-BR.json'
        }
    });    
});







