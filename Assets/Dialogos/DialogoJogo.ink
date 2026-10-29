```ink
VAR resultado_dialogo = ""

// #panel -dialogue -thought
// #speaker
// codigo de falas da historia = "semana"_"dia"_"hora"_"local"
// codigo de falas tutorial = "nome do minigame"_tutorial
// codigo de falas fim de minigame = "nome do minigame"_concluido
// codigo de falas especiais = "nome da cena especial"
// codigo de falas

===sonho===

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

===1_segunda_6_quarto===

Argh... o alarme tá tocando... #panel:dialogue #speaker:RobinBravo
Eu deveria me levantar... mas só mais 5 minutos não vai matar ninguém... #panel:dialogue #speaker:RobinNormal
Eu demorei pra pegar no sono essa noite... e ainda teve esse sonho de novo... #panel:dialogue #speaker:RobinNormal
Será que eu realmente estou tentando o meu melhor...? #panel:dialogue #speaker:RobinNormal

+ Desligar o despertador e se levantar da cama.
    ~ resultado_dialogo = "despertador_levantar"
    -> END

+ Adiar o despertador e dormir mais.
    ~ resultado_dialogo = "despertador_snooze"
    -> END

=== rota_1_manha ===

// Robin se levanta da cama e abre a cortina do quarto.

Ah, mais um dia... #panel:thought
Vamos lá, Robin! Você só precisa se levantar, fazer suas necessidades básicas e ir para a faculdade... #panel:thought
Basicamente só sobreviver... Não é tão difícil assim, certo? #panel:thought
Depois de escutar aquela conversa, eu acabei instalando um aplicativo de tarefas para me organizar melhor... #panel:thought
Eu deveria acessar meu celular para verificar as tarefas que eu anotei para hoje...  #panel:thought
Minhas aulas só começam às 09:00... eu preciso sair às 08:30 para encontrar com a Soleil no metrô... mas eu tenho tempo ainda para fazer algumas tarefas antes... #panel:thought
Eu deveria abrir o aplicativo TO DO... #panel:thought

-> END

===bloco_tarefas_tutorial===
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

// Depois da manhã, Robin volta para o quarto para se trocar e ir para a faculdade.

Não é como se eu quisesse ir para a faculdade... #panel:dialogue #speaker:RobinNormal
Ainda assim, eu preciso ir... é só colocar um sorriso no rosto... só isso... #panel:dialogue #speaker:RobinNormal
Eu deveria me trocar... #panel:dialogue #speaker:RobinNormal

-> END

// Aqui pode escurecer a tela enquanto Robin se troca.
// Depois disso, o jogo pode forçar o jogador a ir até a porta principal.

===depois_arrumado===

É só mais um dia... é só a faculdade... vai ficar tudo bem... #panel:dialogue #speaker:RobinNormal
Mas vai ter tanta gente lá... e eu ainda preciso pegar o metrô agora de manhã... vai estar lotado... #panel:dialogue #speaker:RobinNormal
Se acalma, Robin... vai ficar tudo bem... é só colocar um sorriso no rosto e agir como sempre... #panel:dialogue #speaker:RobinNormal
E você vai encontrar a Soleil no metrô, você não vai estar sozinho... #panel:dialogue #speaker:RobinNormal

-> END

