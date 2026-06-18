```ink
VAR resultado_dialogo = ""


=== primeiro_dia ===

// CENA 1 – PRIMEIRO DIA, SEGUNDA-FEIRA
// INT. QUARTO DE ROBIN
// O despertador toca. Robin acorda em sua cama, cansado.

Argh... o alarme tá tocando... #panel:dialogue #speaker:RobinBravo
Eu deveria me levantar... mas só mais 5 minutinhos não vai matar ninguém... #panel:dialogue #speaker:RobinNormal
Eu mal consegui dormir noite passada... #panel:thought

O que Robin deve fazer? #panel:thought

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
Basicamente, só sobreviver... #panel:thought

-> END

=== depois_atividade_comer ===

Robin prepara alguma coisa rápida para comer antes da faculdade. #panel:thought
Pelo menos agora eu não vou sair completamente vazio... #panel:thought

-> END


=== depois_atividade_banho ===

A água cai sobre Robin enquanto ele tenta acordar de verdade. #panel:thought
Certo... isso ajudou um pouco. #panel:thought

-> END


=== depois_atividade_gato ===

Robin coloca comida para Café, que se aproxima imediatamente. #panel:thought
Pronto, pequeno. Pelo menos um de nós vai começar o dia feliz. #panel:thought

-> END


=== depois_atividade_doomscrolling ===

Robin pega o celular por alguns minutos. Alguns minutos viram mais do que ele gostaria. #panel:thought
Eu devia ter feito qualquer outra coisa... #panel:thought

-> END


=== depois_atividade_plantas ===

Robin rega as plantas com cuidado. #panel:thought
Pelo menos elas parecem estar indo bem... #panel:thought

-> END

=== depois_atividade_lousa ===

Robin lava a louça. #panel:thought
Ainda bem que acbou, lavar isso tudo é um saco, não posso deixar acumular tanto assim. #panel:thought

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

// Aqui pode escurecer a tela enquanto Robin se troca.
// Depois disso, o jogo pode forçar o jogador a ir até a porta principal.

É só mais um dia... é só a faculdade... vai ficar tudo bem... #panel:dialogue #speaker:RobinNormal
Mas vai ter tanta gente lá... e eu ainda preciso pegar o metrô agora de manhã... vai estar lotado... #panel:dialogue #speaker:RobinNormal
Se acalma, Robin... vai ficar tudo bem... é só colocar um sorriso no rosto e agir como sempre... #panel:dialogue #speaker:RobinNormal
E você vai encontrar a Soleil no metrô, você não vai estar sozinho... #panel:dialogue #speaker:RobinNormal

-> END


=== estacao_metro ===

// INT. ESTAÇÃO DE METRÔ
// Robin chega na estação e encontra Soleil.

Oi Robin! Eu tava te esperando! #panel:dialogue #speaker:SoleilNormal
Oi Sol... desculpa, eu te fiz esperar muito? #panel:dialogue #speaker:RobinNormal
Não, eu cheguei faz uns 5 minutos, não se preocupa. #panel:dialogue #speaker:SoleilNormal
Pronte para ir? #panel:dialogue #speaker:SoleilNormal

Não, eu nunca vou estar... mas eu não quero ser o motivo da Soleil chegar atrasada na aula... #panel:thought

Sim, vamos lá... #panel:dialogue #speaker:RobinNormal
Ei, vai ficar tudo bem, Robin. Eu tô aqui com você. #panel:dialogue #speaker:SoleilNormal
Eu sei, Sol... você é a melhor amiga de todas. #panel:dialogue #speaker:RobinNormal

-> END

// Robin e Soleil entram no vagão do metrô.
// O vagão está cheio e barulhento.
// Robin começa a ter um ataque de pânico.

=== estacao_vagao ===

Robin, tá tudo bem... só respira comigo, tá bom...? #panel:dialogue #speaker:SoleilNormal

-> END


=== depois_minigame_ansiedade ===

// Ao fim do minigame, Robin e Soleil saem do vagão.

Tá tudo bem...? #panel:dialogue #speaker:SoleilNormal
Sim, só... tinha muita gente lá... mas eu já tô bem, sério... #panel:dialogue #speaker:RobinNormal
Se você diz... vamos para a faculdade então... #panel:dialogue #speaker:SoleilNormal

// A tela fica preta por enquanto, já que ainda não há imagem do campus.

Eu preciso ir para a minha sala, mas eu te vejo depois, certo? #panel:dialogue #speaker:SoleilNormal
Claro, você não vai se livrar de mim tão fácil assim... #panel:dialogue #speaker:RobinNormal

Eu preciso ir para a minha sala também... apesar de eu não querer... aula de segunda é sempre um saco... #panel:thought

-> END


=== depois_aula ===

// Após a aula terminar, Robin se encontra com Soleil.

Oiê Robin! Eu voltei! #panel:dialogue #speaker:SoleilNormal
Você não sabe quem eu me sentei do lado hoje... #panel:dialogue #speaker:SoleilNormal
Pra você estar tão animada pra me contar... o Kai...? #panel:dialogue #speaker:RobinNormal
Mas ele não é da sua sala já? Vocês não se encontram todos os dias? #panel:dialogue #speaker:RobinNormal

Ah, sim, mas ele normalmente tá sempre cercado de gente, então é um milagre que o lugar do seu lado ainda estivesse vazio... #panel:dialogue #speaker:SoleilNormal
A gente passou a aula inteira conversando, ele é um fofo. #panel:dialogue #speaker:SoleilNormal
Eu super entendo por que você gosta dele, mas quando é que você vai ter coragem de falar com ele? #panel:dialogue #speaker:SoleilNormal

Sol, você sabe que não é tão fácil assim... #panel:dialogue #speaker:RobinNormal
Além do mais... ele está sempre cercado de pessoas... definitivamente ele pelo menos gosta de um deles... #panel:dialogue #speaker:RobinNormal

E se esse “um deles” for você? Nunca se sabe... #panel:dialogue #speaker:SoleilNormal
Ai Sol, só você mesmo... por que ele gostaria de alguém como eu...? #panel:dialogue #speaker:RobinNormal
Robin, tem muitas coisas pra se gostar em você... #panel:dialogue #speaker:SoleilNormal
Você sempre fala isso... meus pais também... não quer dizer que eu concordo... #panel:dialogue #speaker:RobinNormal

-> END
```
