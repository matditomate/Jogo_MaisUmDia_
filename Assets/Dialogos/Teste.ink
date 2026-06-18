VAR nota = 0

=== inicio ===

Professor: O que é design?

+ Arte
    Professor: Não exatamente.
    -> fim

+ Solução de problemas
    ~ nota++
    Professor: Correto!
    -> fim

=== fim ===

{ nota > 0:
    Você acertou.
- else:
    Você errou.
}

-> END