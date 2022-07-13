# Trabalho (III Unidade)

Implementação do Analisador Léxico/Sintático para uma Calculadora que aceite Números Romanos

1. Implemente um Analisador Léxico que leia um arquivo contendo um programa fonte para uma calculadora
   que aceite números romanos no intervalo [I,C]; as operações de soma, subtração, multiplicação e divisão; o uso de parêntesis; e produza um arquivo intermediário contendo uma sequência de tokens que represente o
   referido programa;
2. Implemente um Analisador Sintático que leia o arquivo intermediário contendo tokens e, a partir de uma
   parser table, identifique se o código fonte está, ou não, correto.
3. Produza um vídeo com até 15 minutos explicando o código produzido. O vídeo poderá ser armazenado no
   Teams, YouTube ou em qualquer outra plataforma, sendo disponibilizado um link para o professor;

## Valid Entries

- "I+I="
- "VII\*L/C-III="
- "(I-II)\*III ="
- "VI\*(V/(VI+VII))"

## Grammar

```txt
S -> E
E -> M | D | A | N | (E)
M -> T * T
D -> T / T
A -> T + T
N -> T - T
T -> id | num
```

## Lexemas

- Identifier
- Operator
- Constant
- Keyword
- Literal
- Puntuator
- Spacial Character
