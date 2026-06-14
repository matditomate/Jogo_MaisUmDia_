```ink
VAR fome_robin = 0
VAR energia_robin = 0
VAR ansiedade_robin = 0
VAR progresso_robin = 0
VAR higiene_robin = 0

VAR fome_cafe = 0
VAR carinho_cafe = 0
VAR agua_plantas = 0

VAR atividades_feitas = 0

VAR fez_comida = false
VAR tomou_banho = false
VAR alimentou_gato = false
VAR fez_doomscrolling = false
VAR regou_plantas = false


=== primeiro_dia ===

// CENA 1 – PRIMEIRO DIA, SEGUNDA-FEIRA
// INT. QUARTO DE ROBIN
// O despertador toca. Robin acorda em sua cama, cansado.

Argh... o alarme tá tocando... #panel:dialogue #speaker:ROBIN
Eu deveria me levantar... mas só mais 5 minutinhos não vai matar ninguém... #panel:dialogue #speaker:ROBIN
Eu mal consegui dormir noite passada... #panel:dialogue #speaker:ROBIN

O que Robin deve fazer? #panel:thought

+ Desligar o despertador e se levantar da cama.
    ~ progresso_robin += 2
    -> rota_1_manha

+ Adiar o despertador e dormir mais.
    ~ fome_robin += 3
    ~ energia_robin += 2
    ~ ansiedade_robin += 1
    ~ progresso_robin -= 2
    -> rota_2_atrasado


=== rota_1_manha ===

// Robin se levanta da cama e abre a cortina do quarto.

Ah, mais um dia... #panel:thought
Vamos lá, Robin! Você só precisa se levantar, fazer suas necessidades básicas e ir para a faculdade... #panel:thought
Basicamente, só sobreviver... #panel:thought

-> escolher_atividade_manha


=== escolher_atividade_manha ===

{atividades_feitas < 3:
    O que Robin vai fazer agora? #panel:thought

    + {fez_comida == false} Fazer comida e comer café da manhã.
        ~ fez_comida = true
        ~ atividades_feitas += 1

        ~ fome_robin -= 4
        ~ energia_robin += 3
        ~ progresso_robin += 1

        Robin prepara alguma coisa rápida para comer antes da faculdade. #panel:thought
        Pelo menos agora eu não vou sair completamente vazio... #panel:thought
        -> escolher_atividade_manha

    + {tomou_banho == false} Tomar banho.
        ~ tomou_banho = true
        ~ atividades_feitas += 1

        ~ higiene_robin += 3
        ~ energia_robin += 1
        ~ progresso_robin += 1

        A água cai sobre Robin enquanto ele tenta acordar de verdade. #panel:thought
        Certo... isso ajudou um pouco. #panel:thought
        -> escolher_atividade_manha

    + {alimentou_gato == false} Alimentar o Café.
        ~ alimentou_gato = true
        ~ atividades_feitas += 1

        ~ fome_cafe -= 4
        ~ carinho_cafe += 2

        Robin coloca comida para Café, que se aproxima imediatamente. #panel:thought
        Pronto, pequeno. Pelo menos um de nós vai começar o dia feliz. #panel:thought
        -> escolher_atividade_manha

    + {fez_doomscrolling == false} Ficar olhando redes sociais.
        ~ fez_doomscrolling = true
        ~ atividades_feitas += 1

        ~ ansiedade_robin += 2
        ~ progresso_robin -= 2

        Robin pega o celular por alguns minutos. Alguns minutos viram mais do que ele gostaria. #panel:thought
        Eu devia ter feito qualquer outra coisa... #panel:thought
        -> escolher_atividade_manha

    + {regou_plantas == false} Regar as plantas.
        ~ regou_plantas = true
        ~ atividades_feitas += 1

        ~ agua_plantas += 4

        Robin rega as plantas com cuidado. #panel:thought
        Pelo menos elas parecem estar indo bem... #panel:thought
        -> escolher_atividade_manha

- else:
    -> preparar_para_faculdade
}


=== rota_2_atrasado ===

// Robin acorda atrasado para a aula, sem tempo para fazer nada além de se trocar.

Merda! Eu estou atrasado! Isso definitivamente foi mais do que 5 minutos... #panel:dialogue #speaker:ROBIN
Eu preciso me levantar... #panel:dialogue #speaker:ROBIN

-> preparar_para_faculdade


=== preparar_para_faculdade ===

// Depois da manhã, Robin volta para o quarto para se trocar e ir para a faculdade.

Não é como se eu quisesse ir para a faculdade... #panel:dialogue #speaker:ROBIN
Ainda assim, eu preciso ir... é só colocar um sorriso no rosto... só isso... #panel:dialogue #speaker:ROBIN
Eu deveria me trocar... #panel:dialogue #speaker:ROBIN

// Aqui pode escurecer a tela enquanto Robin se troca.
// Depois disso, o jogo pode forçar o jogador a ir até a porta principal.

É só mais um dia... é só a faculdade... vai ficar tudo bem... #panel:dialogue #speaker:ROBIN
Mas vai ter tanta gente lá... e eu ainda preciso pegar o metrô agora de manhã... vai estar lotado... #panel:dialogue #speaker:ROBIN
Se acalma, Robin... vai ficar tudo bem... é só colocar um sorriso no rosto e agir como sempre... #panel:dialogue #speaker:ROBIN
E você vai encontrar a Soleil no metrô, você não vai estar sozinho... #panel:dialogue #speaker:ROBIN

-> estacao_metro


=== estacao_metro ===

// INT. ESTAÇÃO DE METRÔ
// Robin chega na estação e encontra Soleil.

Oi Robin! Eu tava te esperando! #panel:dialogue #speaker:SOLEIL
Oi Sol... desculpa, eu te fiz esperar muito? #panel:dialogue #speaker:ROBIN
Não, eu cheguei faz uns 5 minutos, não se preocupa. #panel:dialogue #speaker:SOLEIL
Pronte para ir? #panel:dialogue #speaker:SOLEIL

Não, eu nunca vou estar... mas eu não quero ser o motivo da Soleil chegar atrasada na aula... #panel:thought

Sim, vamos lá... #panel:dialogue #speaker:ROBIN
Ei, vai ficar tudo bem, Robin. Eu tô aqui com você. #panel:dialogue #speaker:SOLEIL
Eu sei, Sol... você é a melhor amiga de todas. #panel:dialogue #speaker:ROBIN

// Robin e Soleil entram no vagão do metrô.
// O vagão está cheio e barulhento.
// Robin começa a ter um ataque de pânico.

Robin, tá tudo bem... só respira comigo, tá bom...? #panel:dialogue #speaker:SOLEIL

// AQUI ENTRA O MINIGAME DO ATAQUE DE ANSIEDADE.
// Você pode criar depois uma tag especial para chamar o minigame, tipo:
// #event:minigame_ansiedade

-> depois_minigame_ansiedade


=== depois_minigame_ansiedade ===

// Ao fim do minigame, Robin e Soleil saem do vagão.

Tá tudo bem...? #panel:dialogue #speaker:SOLEIL
Sim, só... tinha muita gente lá... mas eu já tô bem, sério... #panel:dialogue #speaker:ROBIN
Se você diz... vamos para a faculdade então... #panel:dialogue #speaker:SOLEIL

// A tela fica preta por enquanto, já que ainda não há imagem do campus.

Eu preciso ir para a minha sala, mas eu te vejo depois, certo? #panel:dialogue #speaker:SOLEIL
Claro, você não vai se livrar de mim tão fácil assim... #panel:dialogue #speaker:ROBIN

Eu preciso ir para a minha sala também... apesar de eu não querer... aula de segunda é sempre um saco... #panel:thought

-> sala_de_aula


=== sala_de_aula ===

// INT. SALA DE AULA
// AQUI ENTRA O MINIGAME DA AULA.
// Você pode criar depois uma tag especial para chamar esse minigame, tipo:
// #event:minigame_aula

-> depois_aula


=== depois_aula ===

// Após a aula terminar, Robin se encontra com Soleil.

Oiê Robin! Eu voltei! #panel:dialogue #speaker:SOLEIL
Você não sabe quem eu me sentei do lado hoje... #panel:dialogue #speaker:SOLEIL
Pra você estar tão animada pra me contar... o Kai...? #panel:dialogue #speaker:ROBIN
Mas ele não é da sua sala já? Vocês não se encontram todos os dias? #panel:dialogue #speaker:ROBIN

Ah, sim, mas ele normalmente tá sempre cercado de gente, então é um milagre que o lugar do seu lado ainda estivesse vazio... #panel:dialogue #speaker:SOLEIL
A gente passou a aula inteira conversando, ele é um fofo. #panel:dialogue #speaker:SOLEIL
Eu super entendo por que você gosta dele, mas quando é que você vai ter coragem de falar com ele? #panel:dialogue #speaker:SOLEIL

Sol, você sabe que não é tão fácil assim... #panel:dialogue #speaker:ROBIN
Além do mais... ele está sempre cercado de pessoas... definitivamente ele pelo menos gosta de um deles... #panel:dialogue #speaker:ROBIN

E se esse “um deles” for você? Nunca se sabe... #panel:dialogue #speaker:SOLEIL
Ai Sol, só você mesmo... por que ele gostaria de alguém como eu...? #panel:dialogue #speaker:ROBIN
Robin, tem muitas coisas pra se gostar em você... #panel:dialogue #speaker:SOLEIL
Você sempre fala isso... meus pais também... não quer dizer que eu concordo... #panel:dialogue #speaker:ROBIN

-> END
```
