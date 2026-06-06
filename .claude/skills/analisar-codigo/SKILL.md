---
name: analisar-codigo
description: Analisa código C# com foco em Clean Architecture, SOLID e Performance.
---

Analise o código C# fornecido seguindo rigorosamente estes critérios:

## 1. Propósito e Arquitetura
- Identifique a responsabilidade principal da classe/método em uma frase curta.
- Verifique se a localização do arquivo condiz com sua responsabilidade (ex: Regras de negócio devem estar no Domain).

## 2. Checklist de Qualidade (Responsabilidades)
- **SOLID:** O código viola o Princípio de Responsabilidade Única (SRP)?
- **Clean Code:** Os nomes de variáveis e métodos são semânticos?
- **Injeção de Dependência:** As dependências estão sendo injetadas via construtor ou há acoplamento rígido?

## 3. Plano de Melhoria (Action Items)
- Liste no máximo 3 pontos críticos de melhoria.
- Para cada ponto, apresente o **Snippet de código refatorado** comparando o "Antes" vs "Depois".

## Restrições de Saída:
- Use uma linguagem técnica e direta.
- Não use explicações genéricas; foque em problemas concretos no código analisado.
- Responda sempre em Markdown estruturado.