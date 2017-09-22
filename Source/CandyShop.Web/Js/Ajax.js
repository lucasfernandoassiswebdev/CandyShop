var AjaxJs = (function ($) {
	var url = {}; //objeto que recebe o nome e endereço da pagina

	// Lista de objetos que guarda o nome e o endereco da pagina, sã carregados na pagina padrao
	var init = function (config) {
		url = config;
		main();
	};

	//carrega a pagina de inicio
	function main() {
		//pega a view main e a carrega no div
		$.get(url.main).done(function (data) {
			$("#DivGrid").slideUp(function () {
				//desce  o divgrid  
				$('#DivGrid').hide().html(data).slideDown();
			});
			//xhr é o código do erro, que é retornado caso o get não tenha sucesso
		}).fail(function (xhr) {
			console.log(xhr.responseText);
		});
	}

	//função que vai carregar o corpo inteiro da pagina
	function carregaPadrao() {
		$.get(url.padrao)
			.done(function (data) {
				$('body').slideUp(function () {
					$('body').hide().html(data).slideDown();
				});
			}).fail(function (xhr) {
				console.log(xhr.responseText);
			});
	}

	function deslogar() {
		$.get(url.logOff).done(function (data) {
			$('body').slideUp(function () {
				$('body').hide().html(data).slideDown();
			});
		}).fail(function (xhr) {
			console.log(xhr.responseText);
		});
	}

	//Função genérica para carregar o div, de acordo com o endereço passado
	function chamaPagina(endereco) {
		//data é o conteudo da view
		$.get(endereco).done(function (data) {
			//a div é recolhida
			$('#DivGrid').slideUp(function () {
				//escondida, carregada e demonstrada novamente                
				$('#DivGrid').hide().html(data).slideDown();
			});
		}).fail(function (xhr) {
			console.log(xhr.responseText);
		});
	}

	function chamaPaginaComIdentificador(endereco, identificador) {
		$.get(endereco, identificador).done(function (data) {
			$('#DivGrid').slideUp(function () {
				$('#DivGrid').hide().html(data).slideDown();
			});
		}).fail(function (xhr) {
			console.log(xhr.responseText);
		});
	}

	function concluirAcao(endereco, objeto, tela) {
		$.post(endereco, objeto)
			//passar o parametro data aqui quando for definida a mensagem 
			.done(function (message) {
				chamaPagina(tela);
				Materialize.toast(message, 3000);
			})
			.fail(function (xhr) {
				console.log(xhr.responseText);
			});
	}

	function concluirAcaoEdicao(endereco, objeto, tela) {
		$.ajax({
			url: endereco,
			type: 'PUT',
			data: objeto,
			success: function (message) {
				chamaPagina(tela);
				Materialize.toast(message, 1500);
			}
		});
	}

	var voltarInicio = function () {
		main();
	};

	//gerenciamento da lojinha
	var historicoCompra = function () {
		chamaPagina(url.historicoCompra);
	};
	var mostraSaldo = function () {
		chamaPagina(url.mostraSaldo);
	};
	//pagamentos
	var detalhePagamento = function () {
		chamaPagina(url.detalhePagamento);
	};
	var listarPagamento = function () {
		chamaPagina(url.listarPagamento);
	};
	var listarPagamentoMes = function (mes) {
		var parametro = { mes: mes };
		chamaPaginaComIdentificador(url.listarPagamentoMes, parametro);
	}
	var listarPagamentoSemana = function() {
		chamaPagina(url.listarPagamentoSemana);
	};

	var listarPagamentoDia = function () {
		chamaPagina(url.listarPagamentoDia);
	};

	var inserirPagamento = function () {
		chamaPagina(url.inserirPagamento);
	};
	var concluirPagamento = function () {
		var pagamento = { ValorPagamento: $('#valorPago').val() };
		concluirAcao(url.concluirPagamento, pagamento, url.listarPagamento);	    
	};
	//usuarios
	var cadastroUsuario = function () {
		chamaPagina(url.cadastroUsuario);
	};
	var listaUsuario = function () {
		chamaPagina(url.listaUsuario);
	};
	var editarUsuario = function (cpf) {
		var usuario = { Cpf: cpf };
		chamaPaginaComIdentificador(url.editarUsuario, usuario);
	};
	var detalheUsuario = function (cpf) {
		var usuario = { Cpf: cpf };
		chamaPaginaComIdentificador(url.detalheUsuario, usuario);
	};
	var concluirCadastroUsuario = function (imgBase64) {
		//montantando o objeto que vai chegar no controller
		var usuario = {
			Cpf: $('#cpf').val(),
			NomeUsuario: $('#Nome').val(),
			Imagem: imgBase64
		};
		concluirAcao(url.concluirCadastroUsuario, usuario, url.cadastroUsuario);
	};
	var concluirEdicaoUsuario = function (imgBase64) {
		var usuario = {
			Cpf: $('#Cpf').val(),
			NomeUsuario: $('#Nome').val(),
			SaldoUsuario: $('#SaldoUsuario').val(),
			SenhaUsuario: $('#Password').val(),
			Ativo: $('#Ativo').val(),
			Imagem: imgBase64
		};
		concluirAcaoEdicao(url.concluirEdicaoUsuario, usuario, url.listaUsuario);
	};
	var desativarUsuario = function (cpf) {
		alert(cpf);
		var usuario = {
			Cpf: cpf
		};
		chamaPaginaComIdentificador(url.desativarUsuario, usuario);
	};
	var desativarUsuarioConfirmado = function (cpf) {
		alert(cpf);
		var usuario = { Cpf: cpf };
		concluirAcaoEdicao(url.desativarUsuarioConfirmado, usuario, url.listarUsuarioInativo)
	};
	var listarUsuarioInativo = function () {
		chamaPagina(url.listarUsuarioInativo);
	};
	var listarUsuarioEmDivida = function () {
		chamaPagina(url.listarUsuarioEmDivida);
	};
	var listarUsuarioPorNome = function () {
		var usuario = { Nome: $('#nomeUsuario').val() };
		chamaPaginaComIdentificador(url.listarUsuarioPorNome, usuario);
	};
	var verificaLogin = function () {
		var usuario = { Cpf: $('#cpf').val(), SenhaUsuario: $('#senha').val() };

		$.post(url.verificaLogin, usuario)
			.done(function () {
				carregaPadrao();
			})
			.fail(function (xhr) {
				Materialize.toast(xhr.responseText, 3000);
			});
	};
	var logOff = function () {
		deslogar();
		Materialize.toast("Deslogado", 3000);
	};
	//produtos
	var listaProduto = function () {
		chamaPagina(url.listaProduto);
	};
	var cadastrarProduto = function () {
		chamaPagina(url.cadastrarProduto);
	};
	var concluirCadastroProduto = function (baseA,baseB,baseC) {
		var produto = {
			NomeProduto: $('#NomeProduto').val(),
			PrecoProduto: $('#PrecoProduto').val(),
			QtdeProduto: $('#QtdeProduto').val(),
			Categoria: $('#Categoria').val(),
			ImagemA: baseA,
			ImagemB: baseB,
			ImagemC: baseC
		};
		concluirAcao(url.concluirCadastroProduto, produto, url.cadastrarProduto);
	};
	var detalheProduto = function (id) {
		var produto = { IdProduto: id };
		chamaPaginaComIdentificador(url.detalheProduto, produto);
	};
	var editarProduto = function (id) {
		var produto = { IdProduto: id };
		chamaPaginaComIdentificador(url.editarProduto, produto);
	};
	var concluirEdicaoProduto = function () {
		var produto = {
			IdProduto: $('#IdProduto').val(),
			NomeProduto: $('#NomeProduto').val(),
			PrecoProduto: $('#PrecoProduto').val(),
			QtdeProduto: $('#QtdeProduto').val(),
			Categoria: $('#Categoria').val(),
			Ativo: $('#Ativo').val()
		};
		concluirAcaoEdicao(url.concluirEdicaoProduto, produto, url.listaProduto);
	};
	var desativarProduto = function (id) {
		var produto = { IdProduto: id };
		chamaPaginaComIdentificador(url.desativarProduto, produto);
	};
	var desativarProdutoConfirmado = function (id) {
		var produto = { IdProduto: id };
		concluirAcao(url.desativarProdutoConfirmado, produto, url.listaProduto);
	};
	var listarInativos = function () {
		chamaPagina(url.listarInativos);
	};
	var listarProdutoPorNome = function () {
		var produto = { Nome: $('#nomeProduto').val() };
		chamaPaginaComIdentificador(url.listarProdutoPorNome, produto);
	};
	
	//retorna links para acessar as paginas.
	return {
		//para admin  e usuario
		init: init,
		voltarInicio: voltarInicio,
		//gerenciamento da lojinha
		mostraSaldo: mostraSaldo,
		//pagamento
		listarPagamento: listarPagamento,
		listarPagamentoSemana: listarPagamentoSemana,
		detalhePagamento: detalhePagamento,
		inserirPagamento: inserirPagamento,
		concluirPagamento: concluirPagamento,
		listarPagamentoMes: listarPagamentoMes,
		listarPagamentoDia: listarPagamentoDia,

		//usuario                
		historicoCompra: historicoCompra,
		concluirCadastroUsuario: concluirCadastroUsuario,
		listaUsuario: listaUsuario,
		cadastroUsuario: cadastroUsuario,
		concluirEdicaoUsuario: concluirEdicaoUsuario,
		editarUsuario: editarUsuario,
		detalheUsuario: detalheUsuario,
		desativarUsuario: desativarUsuario,
		desativarUsuarioConfirmado: desativarUsuarioConfirmado,
		listarUsuarioInativo: listarUsuarioInativo,
		listarUsuarioEmDivida: listarUsuarioEmDivida,
		listarUsuarioPorNome: listarUsuarioPorNome,
		verificaLogin: verificaLogin,
		logOff: logOff,
		//produtos
		listaProduto: listaProduto,
		cadastrarProduto: cadastrarProduto,
		concluirCadastroProduto: concluirCadastroProduto,
		detalheProduto: detalheProduto,
		editarProduto: editarProduto,
		concluirEdicaoProduto: concluirEdicaoProduto,
		desativarProduto: desativarProduto,
		desativarProdutoConfirmado: desativarProdutoConfirmado,
		listarInativos: listarInativos,
		listarProdutoPorNome: listarProdutoPorNome
	};

})(jQuery); //O método ajaxJS é auto executado quando é iniciado o sistema.