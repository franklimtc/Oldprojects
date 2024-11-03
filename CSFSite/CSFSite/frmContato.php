

<?php

if (array_key_exists ( 'Enviado', $_POST )) {
	
	$nome = $_POST ['nome'] ;
	$mail = $_POST ['email'];
	$comentario = $_POST ['comentarios'];

	if ($nome != null || $mail != null || $comentario != null) {

		// Coloque a mensagem que irá ser enviada para seu e-mail abaixo:

		//$msg = "Mensagem enviada em " . date ( "d/m/Y" ) . ", os dados seguem abaixo: \n\r";

		/*while ( list ( $campo, $valor ) = each ( $HTTP_POST_VARS ) ) {

			$msg .= ucwords ( $campo ) . ": " . $valor . " \n\r";

		}*/

		// Agora iremos fazer com que o PHP envie os dados do Formulário para seu e-mail:
		
		//INTERVENÇÃO DAVID
		
		$mensagem = 'O cliente de nome '.$nome.' e e-mail '.$mail.' está mandando o seguinte comentário: '.$comentario;
		/*echo $mensagem;*/
		

		$email_remetente = $mail;
		$headers = "MIME-Version: 1.1\n";
		$headers .= "Content-type: text/plain; charset=iso-8859-1\n";
		$headers .= "From: $email_remetente\n"; // remetente
		$headers .= "Return-Path: $email_remetente\n"; // return-path
		$headers .= "Reply-To: $mail\n"; // Endereço (devidamente validado) que o seu usuário informou no contato
		
		$envio = mail("qualidade@csfdigital.com.br,anderson@csfdigital.com.br", "Contato VIA SITE CSF DIGITAL",$mensagem, $headers, "-f$email_remetente");
		/*mail ( "davtechh@gmail.com,anderson@csfdigital.com.br", "Contato VIA SITE CSF DIGITAL", $mensagem);*/

		echo "J&aacute; pode fechar essa janela!";

		echo "<script>alert('Seu e-mail foi enviado com sucesso. Obrigado!')</script>";

		echo "<script type=\"text/javascript\">$('.selector').dialog({close: function(blind, 500) {}});</script>";

		die();

	} else {

		echo "<script>alert('Existem campos em branco, favor preencher todos!')</script>";

	}

}

?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>

<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

<title>Colégio Literato</title>

<style type="text/css">

body {

	font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;

	font-size: 12px;

}

p, h1, form, button {

	border: 0;

	margin: 0;

	padding: 0;

}

.spacer {

	clear: both;

	height: 1px;

}

/* ----------- My Form ----------- */

.myform {

	margin-left:10px;

	width: 400px;

	padding: 14px;

}

/* ----------- stylized ----------- */

#stylized {

	border: solid 2px #b7ddf2;

	background: #666666;

}

#stylized h1 {

	font-size: 14px;

	font-weight: bold;

	margin-bottom: 8px;

}

#stylized p {

	font-size: 11px;

	color: #666666;

	margin-bottom: 20px;

	border-bottom: solid 1px #b7ddf2;

	padding-bottom: 10px;

}

#stylized label {

	display: block;

	font-weight: bold;

	text-align: right;

	width: 140px;

	float: left;

}

#stylized .small {

	color: #666666;

	display: block;

	font-size: 11px;

	font-weight: normal;

	text-align: right;

	width: 140px;

}

#stylized input, #stylized textarea {

	float: left;

	font-size: 12px;

	padding: 4px 2px;

	border: solid 1px #aacfe4;

	width: 200px;

	margin: 2px 0 20px 10px;

}

#stylized button {

	clear: both;

	margin-left: 150px;

	width: 67px;

	height: 26px;

	background: url(SiteBase/i/bt_ok.png) no-repeat;

	text-align: center;

	line-height: 31px;

	color: #FFFFFF;

	font-size: 11px;

	font-weight: bold;

	cursor:pointer;

}

frame {

	border: 0px;

}

</style>

</head>

<body>

<div id="stylized" class="myform">

  <form id="form1" name="form1" method="post" action="">

    <h1>Formulário de Contato</h1>

    <p>Agradecemor pela sua mensagem</p>

    <label>Nome <span class="small">Digite seu nome</span> </label>

    <input

	type="text" name="nome" id="nome" />

    <label>Email <span class="small">Insira

      um e-mail válido</span> </label>

    <input type="text" name="email"

	id="email" />

    <label>Sua mensagem <span class="small">descreva sua

      mensagem</span> </label>

    <textarea name="comentarios" id="comentarios"

	cols="45" rows="5"></textarea>

    <input name="Enviado" type="hidden" id="Enviado" value="ok" />

    <button type="submit"></button>

    <div class="spacer"></div>

  </form>

</div>

<script type="text/javascript">

var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");

document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));

</script>

<script type="text/javascript">

try {

var pageTracker = _gat._getTracker("UA-15470695-1");

pageTracker._trackPageview();

} catch(err) {}</script>

</body>

</html>

