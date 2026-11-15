```ink
// Chamadas a cada knot devem ser feitas pelo DialogueManager/Unity.
// #panel:dialogue  -> balão de diálogo
// #panel:thought   -> pensamento
// #speaker:Nome    -> personagem/retrato
// resultado_dialogo -> identificador que a Unity pode usar ao terminar a fala.
// Cenas e minigames que exigem interação externa terminam com -> END.
// quarta-feira não tem falas; sexta-feira termina incompleta.

=== sonho ===

Vocês mimam o garoto demais! Deixam ele fugir das responsabilidades com aquele discursinho de depressão ou sei lá o que! #panel:dialogue #speaker:Tia
Ele fala que não está bem, mas ele nem faz nada para melhorar dessa suposta “depressão”, faz? #panel:dialogue #speaker:Tia
Não é assim! Elu se esforça sim. #panel:dialogue #speaker:Mae
Seu apartamento está sempre uma zona, quando foi a última vez que elu tomou um banho ou comeu alguma coisa? #panel:dialogue #speaker:Tia
Amor, você vai realmente deixar sua irmã falar assim de nosso filho? #panel:dialogue #speaker:Mae
Mas ela não está completamente errada, está? Você sabe tão bem quanto eu que Robin não está se esforçando para melhorar... #panel:dialogue #speaker:Pai
Isso mesmo! Aposto que ele passa o dia enfurnado no quarto naquele apartamento que vocês deram para ele! #panel:dialogue #speaker:Tia
Passar o dia na cama e não cuidar de si não é se esforçar! Ele só está fazendo drama para chamar a atenção de vocês! Igual aquela vez que ele tentou se- #panel:dialogue #speaker:Tia
Já chega! Você não sabe de nada de Robin! Se você não tem nada de útil para dizer, saia da minha casa! #panel:dialogue #speaker:Mae
Eles estão falando de mim #panel:thought
Querida, ela está certa... Eu não sinto que Robin esteja se esforçando... ele está cada dia pior... #panel:dialogue #speaker:Pai
Você sabe que não é tão simples assim... Eu sei que ele está fazendo o seu melhor... #panel:dialogue #speaker:Mae
Mas aparentemente isso não é suficiente... Eu sinto como se nosso filho estivesse desaparecendo lentamente bem na frente de nossos olhos... #panel:dialogue #speaker:Pai
Eu sei... E eu tenho medo de lhe perder também... #panel:dialogue #speaker:Mae

-> END

=== 1_segunda_6_quarto ===

Argh... o alarme tá tocando... #panel:dialogue #speaker:RobinBravo
Eu deveria me levantar... mas só mais 5 minutos não vai matar ninguém... #panel:dialogue #speaker:RobinNormal
Eu demorei pra pegar no sono essa noite... e ainda teve esse sonho de novo... #panel:dialogue #speaker:RobinNormal
Será que eu realmente estou tentando o meu melhor...? #panel:dialogue #speaker:RobinNormal

+ [Desligar o despertador e se levantar da cama.]
    ~ resultado_dialogo = "despertador_levantar"
    -> END

+ [Adiar o despertador e dormir mais.]
    ~ resultado_dialogo = "despertador_snooze"
    -> END

=== rota_1_manha ===

// Robin se levanta da cama e abre a cortina do quarto.
Ah, mais um dia... #panel:thought
Vamos lá, Robin! Você só precisa se levantar, fazer suas necessidades básicas e ir para a faculdade... #panel:thought
Basicamente só sobreviver... Não é tão difícil assim, certo? #panel:thought
Depois de escutar aquela conversa, eu acabei instalando um aplicativo de tarefas para me organizar melhor... #panel:thought
Eu deveria acessar meu celular para verificar as tarefas que eu anotei para hoje... #panel:thought
Minhas aulas só começam às 09:00... eu preciso sair às 08:30 para encontrar com a Soleil no metrô... mas eu tenho tempo ainda para fazer algumas tarefas antes... #panel:thought
Eu deveria abrir o aplicativo TO DO... #panel:thought

-> END

=== bloco_tarefas_tutorial ===

Eu acabei anotando todas as tarefas que eu preciso fazer normalmente durante a semana... #panel:thought
Mas eu queria testar uma funcionalidade desse aplicativo... Quando eu instalei, eu reparei que era possível selecionar no máximo 3 das minhas tarefas para ele me relembrar ao longo do dia... #panel:thought
Eu deveria escolher uma para começar... eu me preocupo com as outras depois... vamos um passo de cada vez... #panel:thought

-> END

=== rota_2_atrasado ===

// Robin acorda atrasado para a aula, sem tempo para fazer nada além de se trocar.
Merda! Eu estou atrasado! Isso definitivamente foi mais do que 5 minutos... #panel:dialogue #speaker:RobinBravo
Eu preciso me levantar... #panel:dialogue #speaker:RobinNormal

-> END

=== preparar_para_faculdade ===

Não é como se eu quisesse ir para a faculdade... #panel:dialogue #speaker:RobinNormal
Ainda assim, eu preciso ir... é só colocar um sorriso no rosto... só isso... #panel:dialogue #speaker:RobinNormal
Eu deveria me trocar... #panel:dialogue #speaker:RobinNormal

-> END

=== depois_arrumado ===

É só mais um dia... é só a faculdade... vai ficar tudo bem... #panel:dialogue #speaker:RobinNormal
Mas vai ter tanta gente lá... e eu ainda preciso pegar o metrô agora de manhã... vai estar lotado... #panel:dialogue #speaker:RobinNormal
Se acalma, Robin... vai ficar tudo bem... é só colocar um sorriso no rosto e agir como sempre... #panel:dialogue #speaker:RobinNormal
E você vai encontrar a Soleil no metrô, você não vai estar sozinho... #panel:dialogue #speaker:RobinNormal

-> END

=== 1_segunda_metro ===

Oi Robin! Eu tava te esperando! #panel:dialogue #speaker:Soleil
Oi Sol... desculpa, eu te fiz esperar muito...? #panel:dialogue #speaker:Robin
Não, eu cheguei faz uns 5 minutos, não se preocupa. #panel:dialogue #speaker:Soleil
Pronte para ir? #panel:dialogue #speaker:Soleil
Não, eu nunca vou estar... mas não quero ser o motivo da Soleil chegar atrasada na aula... #panel:thought
Sim, vamos lá... #panel:dialogue #speaker:Robin
Ei, vai ficar tudo bem, Robin. Eu tô aqui com você. #panel:dialogue #speaker:Soleil
Eu sei, Sol... Você é a melhor amiga de todas... #panel:dialogue #speaker:Robin

-> END

=== 1_segunda_vagao ===

Tá tudo bem? Só... respira comigo, tá bom...? Igual a gente sempre faz... #panel:dialogue #speaker:Soleil

-> END

=== 1_segunda_metro2 ===

Como você tá...? #panel:dialogue #speaker:Soleil
Eu tô bem... só tinha muita gente lá... mas eu já tô bem, sério. #panel:dialogue #speaker:Robin
Se você diz... Vamos para a faculdade então! #panel:dialogue #speaker:Soleil

-> END

=== 1_segunda_faculadade ===

Chegamos... Eu preciso ir para a minha aula, mas eu te vejo depois, certo? #panel:dialogue #speaker:Soleil
Claro, você não vai se livrar de mim tão fácil assim... #panel:dialogue #speaker:Robin
Eu preciso ir para a minha aula também... apesar de eu não querer... aula de segunda é sempre um saco... #panel:thought

-> END

=== 1_segunda_salaAula ===

Oiê Robin! Estou de volta! #panel:dialogue #speaker:Soleil
Eu tive aula com o Kai hoje e eu consegui sentar do lado dele! #panel:dialogue #speaker:Soleil
Que bom, ele tá sempre cercado de gente, mesmo sendo nosso amigo, é tão difícil falar com ele sem ninguém interromper... #panel:dialogue #speaker:Robin
Sim, mas ele é sempre um fofo, Robin! Eu só queria que vocês dois namorassem logo! #panel:dialogue #speaker:Soleil
Calma aí, Sol! Não é tão fácil assim, você sabe... Além do mais, você sabe quantas pessoas ele conhece... duvido muito que ele não goste de nenhuma delas... #panel:dialogue #speaker:Robin
Sim, Robin, você! Ele te conhece também, não conhece? Nós somos amigos desde pequenininhos Robin! Esse "uma delas" pode muito bem ser você... #panel:dialogue #speaker:Soleil
Nunca se sabe... E eu vejo o jeito que ele te olha, Robin... Se aquilo não é o olhar de alguém apaixonado, eu não sei o que é... #panel:dialogue #speaker:Soleil
Robin, tem muitas coisas pra se gostar em você. #panel:dialogue #speaker:Soleil
Você sempre fala isso... meus pais também, não quer dizer que eu concordo... #panel:dialogue #speaker:Robin
Robin, o que eu posso fazer pra fazer você se enxergar do mesmo jeito que eu te vejo...? #panel:dialogue #speaker:Soleil

+ [Eu tento, Sol... mas é complicado, você não tem a minha cabeça...]
    ~ resultado_dialogo = "nao_fugir"
    -> END

+ [Vamos mudar de assunto, sim? Eu não tô com cabeça pra falar disso agora...]
    ~ resultado_dialogo = "fugir"
    -> END

=== rota_1_salaAula ===

Robin... eu sei que você sempre dá o seu melhor pra tudo... #panel:dialogue #speaker:Soleil

-> END

=== rota_2_salaAula ===

Você nunca tá... mas você sabe que eu nunca te forçaria a nada... #panel:dialogue #speaker:Soleil
E eu sempre vou estar aqui se você quiser conversar... #panel:dialogue #speaker:Soleil

-> END

=== 1_segunda_salaAula_continuacao ===

Você é uma pessoa incrível, eu não poderia querer um amigue melhor que você... #panel:dialogue #speaker:Soleil
O que você e o Kai conversaram sobre...? #panel:dialogue #speaker:Robin
Tá, tá! Já entendi que você não quer falar disso! Mas minhas palavras continuam sendo verdade... #panel:dialogue #speaker:Soleil
Eu convidei ele pra almoçar com a gente hoje, mas ele já tinha um rolê marcado ai com uns amigos dele. Iam almoçar em algum lugar aí e depois ir pra pista de skate. #panel:dialogue #speaker:Soleil
Mas! Ele falou que amanhã ele tá livre, então nós podemos almoçar juntos! #panel:dialogue #speaker:Soleil
Mas Sol... Nós já vamos ter aquele almoço com todo mundo na quarta que você vai me obrigar a ir... #panel:dialogue #speaker:Robin
Sim, mas esse é diferente, vai ser só nós três... #panel:dialogue #speaker:Soleil
E eu não vou te obrigar, só te irritar até você ir comigo! #panel:dialogue #speaker:Soleil
Você é inacreditável... #panel:dialogue #speaker:Robin
E aí, amanhã rola ou não? #panel:dialogue #speaker:Soleil

+ [Tudo bem, eu vou almoçar amanhã com vocês...]
    ~ resultado_dialogo = "almoco_aceito"
    Tudo bem, eu vou almoçar amanhã com vocês... #panel:dialogue #speaker:Robin
    -> 1_segunda_almoco_confirmado

+ [Eu não sei, Sol... Eu vou só me envergonhar na frente dele...]
    ~ resultado_dialogo = "almoco_inseguro"
    Eu não sei, Sol... Eu vou só me envergonhar na frente dele... #panel:dialogue #speaker:Robin
    -> 1_segunda_almoco_inseguro

=== 1_segunda_almoco_inseguro ===

Você sabe tão bem quanto eu que isso é impossível... #panel:dialogue #speaker:Soleil
Não importa o que você faça, o Kai vai te achar adorável... #panel:dialogue #speaker:Soleil
Tá bom, eu vou só pra você nunca mais falar coisas desse tipo. #panel:dialogue #speaker:Robin
-> 1_segunda_almoco_confirmado

=== 1_segunda_almoco_confirmado ===

Isso! Finalmente um rolêzinho nosso! #panel:dialogue #speaker:Soleil
Se você quiser, eu posso até dar uma escapada alguma hora para deixar vocês dois sozinhos... #panel:dialogue #speaker:Soleil
NEM OUSE! Eu te odeio, Sol! #panel:dialogue #speaker:Robin
Hahaha! Eu também te amo, Robin! #panel:dialogue #speaker:Soleil
E hoje? Você quer sair para almoçar? #panel:dialogue #speaker:Soleil
Não... eu fiz uma lista de tarefas pra tentar me organizar melhor e eu tenho muita coisa pra fazer... #panel:dialogue #speaker:Robin
Além disso, a gente já vai sair amanhã também... E você sabe que eu não me dou bem com marcar coisas em cima da hora... #panel:dialogue #speaker:Robin
Tá bom, tá bom. Vou me contentar com nosso rolê de amanhã então! #panel:dialogue #speaker:Soleil
Vamos então, eu vou com você até a estação perto do seu apartamento e depois eu vejo se meu namorado quer almoçar comigo em algum lugar. #panel:dialogue #speaker:Soleil

-> END

=== 1_segunda_metro_volta ===

// Soleil e Robin voltaram de metrô. Cena de despedida na estação.
Bom, é aqui que eu me vou! Se cuida, Robin! Não esquece de almoçar, viu? #panel:dialogue #speaker:Soleil
Eu sei, eu sei, Sol... você tá falando igual a minha mãe! #panel:dialogue #speaker:Robin
Porque eu preciso cuidar do meu querido filhinho! #panel:dialogue #speaker:Soleil
Mas é sério, se você precisar de qualquer coisa, me liga, me manda mensagem... #panel:dialogue #speaker:Soleil
E se meu celular estiver sem bateria e eu não tiver como carregar ele? #panel:dialogue #speaker:Robin
Pombo correio, ou grita então, sei lá. #panel:dialogue #speaker:Soleil
Preciso pegar o próximo metrô, te vejo amanhã, certo? #panel:dialogue #speaker:Soleil
Claro, se você não estiver cansada de me aturar... #panel:dialogue #speaker:Robin
Como se isso algum dia fosse acontecer. Até amanhã, Robin! #panel:dialogue #speaker:Soleil
Até, Sol. #panel:dialogue #speaker:Robin

-> END

=== 1_segunda_casa_tarde ===

// Cena de Robin e Café;
Estou de volta, Café! #panel:dialogue #speaker:Robin
Sentiu minha falta? #panel:dialogue #speaker:Robin
Eu preciso almoçar, eu sei que eu preciso... mas eu não estou com vontade de comer nada... #panel:dialogue #speaker:Robin
Ainda assim, eu não quero decepcionar a Sol... ainda mais quando ela fez questão de me lembrar antes de nos despedirmos... #panel:dialogue #speaker:Robin
O que eu deveria fazer, Café? #panel:dialogue #speaker:Robin
Miau! #panel:dialogue #speaker:Cafe
“Ele fala que não está bem, mas ele nem faz nada para melhorar dessa suposta “depressão”, faz? Seu apartamento está sempre uma zona, quando foi a última vez que elu tomou um banho ou comeu alguma coisa?” #panel:thought
Ah... saco, as palavras da minha tia realmente mexeram com a minha cabeça... #panel:thought
Miau? #panel:dialogue #speaker:Cafe
Você está certo, Café! Eu deveria pelo menos tentar comer algo... Eu não posso deixar minha tia ganhar. #panel:dialogue #speaker:Robin

+ [Fazer comida.]
    ~ resultado_dialogo = "almoco_cozinhar"
    -> END

+ [Comida congelada ou sobras.]
    ~ resultado_dialogo = "almoco_sobras"
    -> END

+ [Encomendar algo.]
    ~ resultado_dialogo = "almoco_encomendar"
    -> END

=== 1_segunda_apos_comer_cedo ===

// Usar se Robin se levantou cedo. Depois disso, habilitar opções de descanso/tarefas.
Eu deveria escolher a próxima tarefa que eu vou fazer... ainda tem muitas coisas para serem feitas... #panel:thought
Mas eu também poderia descansar um pouco antes de fazer qualquer coisa... #panel:thought
Estou cansado das aulas entediantes e, só de pensar em precisar sair amanhã e conversar com o Kai, eu me sinto mais exausto ainda... #panel:thought
O que eu deveria fazer...? Não é como se descansar só um pouco fosse fazer muita diferença... Quem sabe até não me ajuda mais, sabe? Aquela lógica de que eu vou ter mais energia do que eu tenho agora... #panel:thought
Eu poderia ler, desenhar... jogar alguma coisa, assistir à TV... ou tirar um cochilo ou verificar minhas redes sociais também... #panel:thought

-> END

=== 1_segunda_apos_comer_atrasado ===

// Usar se Robin adiou o despertador. A Unity deve abrir o celular e exigir acesso ao TO DO.
Ah! Eu tinha me esquecido! #panel:dialogue #speaker:Robin
Depois de escutar aquela conversa dos meus pais, eu acabei instalando um aplicativo de tarefas para me organizar melhor... #panel:dialogue #speaker:Robin
Como eu acordei tarde, sem tempo pra fazer nada de manhã, eu acabei nem me lembrando disso! #panel:dialogue #speaker:Robin
Eu deveria acessar meu celular para verificar as tarefas que eu anotei para hoje... #panel:dialogue #speaker:Robin
// Robin pega seu celular e desbloqueia ele.
Eu deveria abrir o aplicativo TO DO... #panel:dialogue #speaker:Robin

-> END

=== 1_segunda_todo_atrasado ===

// Explicação de tarefas/descanso após o jogador abrir o TO DO.
Eu acabei anotando todas as tarefas que eu preciso fazer normalmente durante a semana... #panel:dialogue #speaker:Robin
Mas eu queria testar uma funcionalidade desse aplicativo... Quando eu instalei, eu reparei que era possível selecionar no máximo 3 das minhas tarefas para ele me relembrar ao longo do dia... #panel:dialogue #speaker:Robin
Eu poderia escolher uma para começar... ou descansar um pouco... Eu estou cansado das aulas entediantes, pegar o metrô lotado também não ajudou nem um pouco com o meu dia... #panel:thought
O que eu deveria fazer...? Não é como se descansar só um pouco fosse fazer muita diferença... Quem sabe até não me ajuda mais, sabe? Aquela lógica de que eu vou ter mais energia do que eu tenho agora... #panel:thought
Eu poderia ler, desenhar... jogar alguma coisa, assistir à TV... ou tirar um cochilo ou verificar minhas redes sociais também... #panel:thought

-> END

=== 2_terca_6_quarto ===

Argh... o alarme tá tocando... de novo... por que eu fui escolher um toque tão irritante...? Pelo menos funciona pra me acordar... #panel:dialogue #speaker:Robin
Parece que não importa quanto eu durma ou se eu tenho sonhos ou não, eu sempre vou acordar sem energia... #panel:dialogue #speaker:Robin
Eu preciso me levantar... mas eu ainda estou com sono... queria tanto poder voltar a dormir... #panel:dialogue #speaker:Robin

+ [Desligar o despertador e se levantar da cama.]
    ~ resultado_dialogo = "terca_despertador_levantar"
    // Unity: progresso +2; liberar manhã e minigames ainda não realizados.
    -> END

+ [Adiar o despertador e dormir mais.]
    ~ resultado_dialogo = "terca_despertador_snooze"
    // Unity: fome +3, energia +2, ansiedade +1, progresso -2; manhã perdida.
    -> END

=== 2_terca_rota_1_manha ===

// Rota ao acordar cedo; sem repetir o tutorial do aplicativo.
Ok, ok... hoje é terça... só tem mais 3 dias (além de hoje, mas a gente ignora essa parte) até o final de semana... #panel:dialogue #speaker:Robin
É só mais um dia... eu só preciso manter o ritmo de ontem... #panel:dialogue #speaker:Robin
Fazer minhas tarefas, ir para a faculdade, almoçar com meus amigos... nada de demais... #panel:dialogue #speaker:Robin
Pelo menos eu não tive nenhum pesadelo hoje... Não foi a melhor noite de sono, até porque eu nunca consigo dormir direito... #panel:dialogue #speaker:Robin
Mas definitivamente foi melhor que ontem... Enfim, vamos lá! #panel:dialogue #speaker:Robin
// Dessa vez não vai ter tutorial, deixar o jogador livre para fazer o que quiser durante a manhã antes do horário da aula, mantendo apenas o tutorial para os minigames que ainda não foram iniciados pela primeira vez.
Apesar de eu não estar com a mínima vontade, está na hora de ir para a faculdade... #panel:dialogue #speaker:Robin

-> END

=== 2_terca_rota_2_atrasado ===

// Rota de quem adiou o alarme; não permitir tarefas matinais.
Ah, merda... eu dormi demais... #panel:dialogue #speaker:Robin
Qual o meu problema... por que eu não consigo só acordar cedo? #panel:dialogue #speaker:Robin
Eu tenho tantas coisas para fazer e ainda assim, eu não consigo nem sair da cama... parece piada até! #panel:dialogue #speaker:Robin
Enfim... eu preciso ir para a faculdade, apesar de eu não estar com muita vontade, não quero me atrasar para a aula também.... #panel:dialogue #speaker:Robin

-> END

=== 2_terca_preparar_faculdade ===

Além do mais, não quero deixar a Sol me esperando sozinha no metrô. #panel:dialogue #speaker:Robin
É só a faculdade... A Sol vai estar comigo na maior parte do tempo... #panel:dialogue #speaker:Robin
E eu vou almoçar com o Kai e a Sol, o que eu honestamente estou dividido entre ser algo bom ou ruim... #panel:dialogue #speaker:Robin
Eu estou com saudades dele, mas ainda assim, eu fico tão nervoso para conversar com ele... #panel:dialogue #speaker:Robin
Mas vai dar tudo certo... Não tem por que se preocupar, Robin... #panel:dialogue #speaker:Robin

-> END

=== 2_terca_metro ===

Bom dia, Sol! Eu te fiz esperar muito? #panel:dialogue #speaker:Robin
Bom dia! Claro que não, e por você, eu esperaria uma eternidade! #panel:dialogue #speaker:Soleil
Hoje nós vamos almoçar com o amor da sua vida! E nem tente escapar! #panel:dialogue #speaker:Soleil
Por favor, só não fala nada do tipo na frente dele... #panel:dialogue #speaker:Robin
Tá bom, mas só porque você pediu com jeitinho. #panel:dialogue #speaker:Soleil
É melhor nós irmos se não queremos chegar atrasados. #panel:dialogue #speaker:Soleil
Tudo bem, desculpa te fazer vir até aqui só pra ir comigo, Sol... Eu sei que é fora do caminho que você faria para chegar na faculdade... #panel:dialogue #speaker:Robin
E eu já te falei mil vezes que eu não me importo, Robin, então não precisa pedir desculpa, tá bem? #panel:dialogue #speaker:Soleil
Eu desisto... você não vai mudar de ideia de qualquer jeito... #panel:dialogue #speaker:Robin
Que bom que você sabe! Agora vamos! #panel:dialogue #speaker:Soleil

-> END

=== 2_terca_vagao ===

//aumentar ansiedade de Robin em 1 ao entrar no vagão.
O mundo vai acabar amanhã! Os aliens vão invadir a Terra! #panel:dialogue #speaker:HomemParanoico
Eles vão usar os chips de controle mental que os reptilianos no poder inseriram em nossos corpos com essas vacinas! #panel:dialogue #speaker:HomemParanoico
Quem dera... #panel:dialogue #speaker:Robin
Ah! Esse maluco tá aqui de novo... #panel:dialogue #speaker:Soleil
Como os seguranças ainda deixam ele entrar no metrô? #panel:dialogue #speaker:Soleil
Tecnicamente eles não podem fazer nada... Afinal de contas, ele nunca fez nada que realmente machucasse alguém... #panel:dialogue #speaker:Robin
Sim, mas ninguém merece escutar isso às oito da manhã! #panel:dialogue #speaker:Soleil
Eu preciso desses documentos para ontem! Como assim você só vai conseguir me entregar semana que vem?! #panel:dialogue #speaker:MulherIrritada
Eu estou te pedindo isso há um mês! UM MÊS! #panel:dialogue #speaker:MulherIrritada
Essas conversas motivam tanto a gente a trabalhar, hein? #panel:dialogue #speaker:Soleil
Nem me fale... encontrar um estágio é tão difícil... #panel:dialogue #speaker:Robin
Só de pensar em ter que lidar com pessoas assim... #panel:dialogue #speaker:Robin
Eu não sei quem é pior... a mulher gritando ou a pessoa que ainda não fez o que ela pediu... Pelo menos essa não é a nossa vida! #panel:dialogue #speaker:Soleil
Ainda... #panel:dialogue #speaker:Robin

-> END

=== 2_terca_metro2 ===

Quase lá... agora é só mais a nossa caminhada típica de 10 minutos até o campus... no calor escaldante dessa nossa maravilhosa cidade... #panel:dialogue #speaker:Soleil
Nossa, que motivador, Sol! #panel:dialogue #speaker:Robin
Conte com a sua coach, Sol, para mais frases motivacionais! #panel:dialogue #speaker:Soleil
Você realmente é inacreditável... #panel:dialogue #speaker:Robin

-> END

=== 2_terca_faculdade ===

Ok, estamos aqui... eu vou vir com o Kai te encontrar depois da aula para nós irmos almoçar juntos! #panel:dialogue #speaker:Soleil
Certo, certo... eu já entendi que não tenho como fugir disso... #panel:dialogue #speaker:Robin
Que bom que você sabe! Okay, eu te vejo mais tarde então! #panel:dialogue #speaker:Soleil
Até mais tarde, Sol. #panel:dialogue #speaker:Robin

-> END

=== 2_terca_almoco_kai ===

// Almoço de terça com Kai e Soleil.
Oiê Robin, estou de volta! E olha quem eu consegui arrastar comigo! #panel:dialogue #speaker:Soleil
Robin, e aí? Quanto tempo! Mesmo que seja só uma semana, mas enfim! Como você tá? #panel:dialogue #speaker:Kai
Oi, Kai. Eu tô bem! Só essas aulas que são um saco... #panel:dialogue #speaker:Robin
Nem me fale! Não vejo a hora que cheguem as férias de novo! Apesar de que com o meu novo estágio, eu nem vou poder descansar direito! #panel:dialogue #speaker:Kai
Ah é! Eu nem falei pra vocês, mas aquele eu passei naquele estágio que eu estava fazendo o processo de entrevistas e tudo mais! #panel:dialogue #speaker:Kai
Que bom, Kai! Então agora você vai poder pagar nosso almoço de hoje! #panel:dialogue #speaker:Robin
Por que todo mundo está conseguindo um estágio, mas eu não...? #panel:thought
Por que eu não consigo nem isso? #panel:thought

+ [Eu realmente sou tão inútil assim...?]
    ~ resultado_dialogo = "estagio_pensamento_negativo"
    Eu realmente sou tão inútil assim...? #panel:thought
    -> 2_terca_almoco_continuacao

+ [Eu só não encontrei a oportunidade certa ainda...]
    ~ resultado_dialogo = "estagio_pensamento_positivo"
    Eu só não encontrei a oportunidade certa ainda... #panel:thought
    -> 2_terca_almoco_continuacao

=== 2_terca_almoco_continuacao ===

Que legal, Kai! Parabéns! #panel:dialogue #speaker:Soleil
-> END

=== 3_quarta_pendente ===
// Não tem nada =(
-> END

=== 4_quinta_6_quarto ===

Que droga! Por que que eu tenho que acordar hoje? #panel:dialogue #speaker:Robin
Queria que desse para simplesmente sumir da Terra. Eu por acaso pedi para nascer?... #panel:dialogue #speaker:Robin

+ [Adiar o despertador e dormir mais.]
    ~ resultado_dialogo = "quinta_despertador_snooze"
    -> END

+ [Adiar o despertador e dormir mais.]
    ~ resultado_dialogo = "quinta_despertador_snooze"
    -> END

=== 4_quinta_10_quarto ===

// Robin acorda às 10h com Café lambendo seu rosto; vê a mensagem de Soleil.
// Unity restringe tarefas: alimentar/acariciar Café, regar plantas, doomscrolling ou voltar a dormir.
Robin, vc está bem? Eu percebi q vc n veio na aula hoje... vc vai vir? #panel:dialogue #speaker:Soleil
Suspiro... estou sem energia para responder a Sol agora, é melhor eu parar de atrapalhar a vida dela ficando em silêncio. #panel:thought
// ROBIN irá se recusar a fazer qualquer tarefa que o jogador tentar fazer, menos:
// Alimentar Café
// Acariciar Café
// Regar as plantas
// Doomscrolling
// Voltar a dormir
-> END

=== 4_quinta_pensamentos ===

// Reflexões de Robin; transição de cena e horário conduzidos pela Unity.
Por que será que eu estou sempre cansade? #panel:thought
Parece que eu nunca estou 100% bem... #panel:thought
Talvez seja porque a minha cabeça devia ser o meu lar quando converso comigo mesmo, mas tudo que faço é me sabotar e me odiar... #panel:thought
Parece que não me sinto em casa dentro da minha própria cabeça, não tem um minuto em que estou feliz aqui #panel:thought
Acho que só me sinto em casa quando estou perto de você, Café! #panel:dialogue #speaker:Robin
Miau! #panel:dialogue #speaker:Cafe
Enfim, acho que vou voltar a dormir, é a única coisa que parece que faço certo... #panel:thought

-> END

=== 4_quinta_cena_especial ===

// Cena sensível às 15:00: Robin está na cama, olhando suas cicatrizes.
// Quando estende a mão em direção ao estilete, uma mensagem de Soleil interrompe a cena.
// A Unity gerencia iluminação, celular e transição.
Ah, como eu odeio essas cicatrizes... #panel:thought
Mas eu estou tão cansado... meu peito dói tanto... eu só queria que essa dor passasse... #panel:thought
Eu não deveria, mas... #panel:thought
Ei #panel:dialogue #speaker:Soleil
o q tá acontecendo? #panel:dialogue #speaker:Soleil
Vc ainda n me respondeu #panel:dialogue #speaker:Soleil

+ [Ta tudo bem, Sol]
    ~ resultado_dialogo = "quinta_mensagem_mentir"
    Ta tudo bem, Sol #panel:dialogue #speaker:Robin
    Eu só acordei atrasade hoje #panel:dialogue #speaker:Robin
    Eu te conheço desde sempre, eu sei quando vc tá mentindo #panel:dialogue #speaker:Soleil
    -> 4_quinta_mensagens_comuns

+ [Eu to cansade, Sol...]
    ~ resultado_dialogo = "quinta_mensagem_verdade"
    Eu to cansade, Sol... #panel:dialogue #speaker:Robin
    Eu não estou mais aguentando tudo isso... #panel:dialogue #speaker:Robin
    Robin... pq vc não me contou? #panel:dialogue #speaker:Soleil
    -> 4_quinta_mensagens_comuns

=== 4_quinta_mensagens_comuns ===

Eu vou dar uma passada aí... #panel:dialogue #speaker:Soleil
A gnt pode fazer o q vc quiser, mas é bom vc abrir a porta pra mim! >:3 #panel:dialogue #speaker:Soleil
// A Unity toca a campainha e libera a porta.
-> END

=== 4_quinta_visita_soleil ===

// Robin abre a porta. Ao terminar, Unity inicia a seleção de filme/brigadeiro.
Ah, ainda estou de pijama... #panel:thought
Não quero me trocar, acho que a Sol já tem intimidade o suficiente para me ver de pijama kk #panel:thought
// Robin vai até a porta e a abre.
Nossa, Robin, você está com uma cara péssima! #panel:dialogue #speaker:Soleil
Também não precisa me deixar mais para baixo do que já estou, né... #panel:dialogue #speaker:Robin
Desculpa, só estou muito preocupada com você. #panel:dialogue #speaker:Soleil
E eu não esqueci do que aconteceu ontem, viu? #panel:dialogue #speaker:Soleil
Eu sinto muito se o que te dissemos te machucou, nós não tínhamos essa intenção! #panel:dialogue #speaker:Soleil
A gente percebeu que você não estava muito confortável, você quer falar sobre isso? #panel:dialogue #speaker:Soleil
Ah, não sei, estou cansade... #panel:dialogue #speaker:Robin
Se você quiser, a gente pode dormir, jogar algum videogame, desenhar, conversar ou apenas ficar em silêncio, mas eu não vou sair de perto de você, não até você se sentir um pouco melhor! #panel:dialogue #speaker:Soleil
Então você não vai sair daqui nunca #panel:dialogue #speaker:Robin
Então eu vou ficar aqui para sempre, não me importo! #panel:dialogue #speaker:Soleil
Não vou deixar você passar por isso sozinhe #panel:dialogue #speaker:Soleil
Mas eu não estou num momento bom para ficar com alguém agora, não quero atrapalhar a vida de ninguém... #panel:dialogue #speaker:Robin
Mas é principalmente nesses momentos que se deve ter uma companhia. Você não está atrapalhando ninguém, você só está passando por um momento difícil agora. Eu tive questão de vir aqui porque você é meu melhor amigue e eu quero ver aquele Robin que eu conheço de humor duvidoso, reclamando da faculdade e falando do seu crush com um sorriso frouxo. #panel:dialogue #speaker:Soleil
Você pode até não gostar desse Robin, mas eu amo e queria que você visse da mesma forma que eu te vejo #panel:dialogue #speaker:Soleil
Vamos fazer assim: agora a gente faz o que você quiser, pode ser até chorar em posição fetal. Eu não vou te forçar a falar nada a não ser que você esteja pronte para me contar, ok? #panel:dialogue #speaker:Soleil
Ok... #panel:dialogue #speaker:Robin
Então o que você quer fazer? #panel:dialogue #speaker:Soleil
Acho que assistir um filme e comer um brigadeiro de panela seria ótimo #panel:dialogue #speaker:Robin
Então vai escolhendo o filme que eu vou fazendo o brigadeiro! #panel:dialogue #speaker:Soleil

-> END

=== 4_quinta_depois_filme ===

// Após duas horas do filme; o jogador escolhe uma maneira de se expressar.
Nossa, você estava precisando mesmo comer um chocolate, você devorou o brigadeiro kk #panel:dialogue #speaker:Soleil
Acho que isso me ajudou a relaxar... muito obrigade, Sol! #panel:dialogue #speaker:Robin
Eu queria ter falado mais antes, mas não estava muito bem para falar na hora, parecia que eu iria explodir de tantos pensamentos... #panel:dialogue #speaker:Robin
Sim, fique tranquile! Às vezes, o melhor a se fazer é deixar a poeira baixar... não adianta ficar nesse turbilhão de pensamentos, isso se torna um ciclo autodepreciativo... você precisa parar e digerir cada pensamento e tirar alguma coisa disso. O que fez você se sentir assim? #panel:dialogue #speaker:Soleil
Ah, não sei... acho que o que vocês disseram ontem no almoço me deu algum gatilho #panel:dialogue #speaker:Robin
Por quê? #panel:dialogue #speaker:Soleil
Me lembrou de uma conversa que escutei entre meus pais e minha tia. Foi na época que eu acho que a minha depressão piorou... Bom, pelo menos o que eu acho que era depressão. #panel:dialogue #speaker:Robin
Minha tia falava que eu não fazia nada para melhorar e ficava invalidando meus sentimentos porque não acreditava que a depressão existia... #panel:dialogue #speaker:Robin
É difícil se sentir assim, parece um paradoxo sem fim. Enquanto eu quero melhorar, não consigo pedir ajuda, mesmo sabendo que preciso. #panel:dialogue #speaker:Robin
Eu... #panel:dialogue #speaker:Robin

+ [Me sinto deprimido e sem motivação.]
    ~ resultado_dialogo = "desabafo_deprimido"
    Me sinto deprimido e sem motivação. #panel:dialogue #speaker:Robin
    -> 4_quinta_depois_desabafo

+ [Acho difícil ter iniciativa para fazer as coisas.]
    ~ resultado_dialogo = "desabafo_iniciativa"
    Acho difícil ter iniciativa para fazer as coisas. #panel:dialogue #speaker:Robin
    -> 4_quinta_depois_desabafo

+ [Sei que não estou bem, eu quero tentar me cuidar.]
    ~ resultado_dialogo = "desabafo_cuidado"
    Sei que não estou bem, eu quero tentar me cuidar. #panel:dialogue #speaker:Robin
    -> 4_quinta_depois_desabafo

=== 4_quinta_depois_desabafo ===

Mas eu não estou culpando vocês por me sentir assim, é só que ouvir o que vocês falaram me desanimou, eu comecei a pensar em muitas coisas e me senti mais incapaz de melhorar... #panel:dialogue #speaker:Robin
Robin, eu estou aqui para te ouvir e te apoiar em qualquer situação e você está certe em avisar que o que nós falamos te incomodou! #panel:dialogue #speaker:Soleil
Nós não tínhamos a intenção de te machucar, mas se isso te incomodou, nós não vamos mais falar isso! #panel:dialogue #speaker:Soleil
Minha psicóloga fala que quando estamos numa crise, principalmente no de ansiedade, você pode usar a tática dos cinco sentidos. #panel:dialogue #speaker:Soleil
Você faz um exercício de respiração antes, tipo mindfulness, tenta desligar um pouco seus pensamentos e faz o seguinte: #panel:dialogue #speaker:Soleil
Olhe a sua volta, sinta onde você está e fale: #panel:dialogue #speaker:Soleil
5 coisas à sua volta que você consegue ver; #panel:dialogue #speaker:Soleil
4 coisas que você pode tocar no momento; #panel:dialogue #speaker:Soleil
3 coisas que você consegue ouvir; #panel:dialogue #speaker:Soleil
2 coisas que você consegue sentir o cheiro; #panel:dialogue #speaker:Soleil
1 coisa que você pode sentir o gosto. #panel:dialogue #speaker:Soleil
A intenção é parar esse monte de pensamentos querendo te sufocar para ancorar no momento presente. #panel:dialogue #speaker:Soleil
Adoro a Sol coach kkkk #panel:dialogue #speaker:Robin
Kkkk mas é sério, isso pode te ajudar nesses momentos tensos, esses pensamentos só vão te destruir se você não parar, relaxar, analisar o que te fez sentir assim e depois tomar uma decisão. #panel:dialogue #speaker:Soleil
E não tenha medo de pedir ajuda, você não está sozinhe nessa. Já falei antes pra você que passei pela depressão, né? Claro que isso varia de cada pessoa, mas parece impossível melhorar mesmo. Parece que procurar ajuda torna o problema “real” ... #panel:dialogue #speaker:Soleil
E essa dificuldade de pedir ajuda não é falta de vontade ou preguiça, isso muitas vezes vem da própria depressão... ela justamente te deixa desmotivado e sem energia #panel:dialogue #speaker:Soleil
Você pode até tentar justificar o motivo pelo qual não se deve pedir ajuda com "há diversos motivos para eu estar bem", mas a questão é que não há motivos para você estar SOFRENDO assim, ninguém merece sofrer com isso, nem o seu pior inimigo! #panel:dialogue #speaker:Soleil
E todos merecem e têm o direito de ser ajudado, inclusive você! #panel:dialogue #speaker:Soleil
E mais uma coisa, você não precisa ter certeza de que tem depressão ou esperar até tudo desmoronar para pedir ajuda! É para isso que serve a terapia, para evitar que isso aconteça. O psicólogo não vai te curar igual ao xarope milagroso da vovó pra dor de garganta, ele vai te orientar para que você mesmo entenda seus pensamentos e suas emoções e se fortaleça! #panel:dialogue #speaker:Soleil
E quem faz terapia não é louco! Na verdade, acho que todos deveriam fazer terapia, porque ela estimula a desenvolver sua responsabilidade emocional e autoconhecimento acerca dos seus sentimentos #panel:dialogue #speaker:Soleil
Enfim, falei muito kk mas você falou tudo que queria falar? #panel:dialogue #speaker:Soleil
Acho que sim... valeu por me ajudar! #panel:dialogue #speaker:Robin
Imagina, é pra isso que servem os amigos! #panel:dialogue #speaker:Soleil
Agora vou te deixar em paz porque já está tarde e precisamos acordar cedo para a nossa divertidíssima aula amanhã! #panel:dialogue #speaker:Soleil
Pois é, nem me fale... boa noite, Sol, muito obrigade por me ouvir e me aconselhar, você é a melhor companhia que alguém poderia ter no fundo do poço! #panel:dialogue #speaker:Robin
Imagina, me chame para o que precisar! Boa noite e não se esquece de tomar o café da manhã heinn #panel:dialogue #speaker:Soleil
Sim, senhora capitã! #panel:dialogue #speaker:Robin
// Robin acompanha Soleil até a porta. Café mia e espirra.
Miau #panel:dialogue #speaker:Cafe
Atchum! #panel:dialogue #speaker:Cafe
Nossa, acho que até o Café percebeu que estou fedendo kk vou tomar um banho e depois vou dormir... #panel:thought
// Unity: banho, Robin vai para a cama e dia termina.
-> END

=== 5_sexta_bar ===

// Roteiro fornecido para sexta-feira acaba nesta conversa; continuação pendente.
Me desculpa pelo que eu falei na quarta, Robin... Eu fui insensível e nem reparei o quão erradas minhas palavras eram, nem como elas podiam te machucar... #panel:dialogue #speaker:Kai
Eu só... eu me importo muito com você e eu tenho muito medo de te perder, Robin... #panel:dialogue #speaker:Kai
Eu... eu já tô acostumado a ouvir essas coisas, principalmente da minha família... eu só não esperava escutar meus amigos falando isso... Doeu bastante, Kai... #panel:dialogue #speaker:Robin
Eu sinto muito, Robin... De verdade... Eu imagino que a Sol já deve ter te contado, mas nós todos conversamos depois que você foi embora... #panel:dialogue #speaker:Kai
A gente se arrependeu do jeito que falamos com você... Mas você é muito importante para nós... A gente não quer te perder... #panel:dialogue #speaker:Kai
Eu sei... mas isso não muda o fato que as palavras de vocês me machucaram bastante... #panel:dialogue #speaker:Robin
Ainda assim, eu não quero brigar com vocês... E eu fui rude também.. #panel:dialogue #speaker:Robin
// A continuação da conversa no bar não consta do material enviado.
-> END
