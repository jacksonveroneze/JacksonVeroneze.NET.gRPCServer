# Análise Consolidada do Projeto ASP.NET Core gRPC Server
**Data:** 2026-06-05  
**Escopo:** Segurança, Performance (EF Core), Cobertura de Testes

---

## 📊 SUMÁRIO EXECUTIVO

| Categoria | Quantidade | Crítico/Alto | Médio | Baixo |
|-----------|-----------|---|---|---|
| **Segurança** | 13 problemas | 5 | 5 | 3 |
| **Performance (EF Core)** | 6 problemas | 3 | 3 | 0 |
| **Testes** | 19 classes | Não testadas | Cobertura <5% | - |
| **Total** | 38 achados | 8 | 8 | 3 |

---

## 🔴 PROBLEMAS CRÍTICOS/ALTOS (Ação Imediata Necessária)

### 1. SEGURANÇA CRÍTICA: Credenciais Hardcoded

**Arquivo:** `/src/main/Api/appsettings.Development.json`

**Problema:**
```json
"ConnectionString": "Host=10.0.0.150;Port=5438;Database=profile_service;Username=postgres;Password=localP@ssword;"
"Endpoint": "10.0.0.150:6379"
"Authority": "https://dev-e1dr7tee5q8cqgzk.us.auth0.com"
```

**Severidade:** 🔴 CRÍTICA  
**Impacto:** Comprometimento completo do banco de dados e serviços internos se o repositório for exposto

**Ação:** 
- [ ] Remover todas as credenciais do appsettings.Development.json
- [ ] Usar `dotnet user-secrets` para desenvolvimento local
- [ ] Implementar Azure Key Vault / AWS Secrets Manager para produção
- [ ] Adicionar `appsettings.*.json` ao `.gitignore`

---

### 2. SEGURANÇA ALTA: Sensitive Data Logging em Produção

**Arquivo:** `/src/main/Infrastructure/Extensions/DatabaseExtensions.cs` (linhas 64-65)

**Problema:**
```csharp
optionsBuilder
    .EnableDetailedErrors()      // Expõe stack traces SQL
    .EnableSensitiveDataLogging() // Registra CPF, dados pessoais
```

**Severidade:** 🟠 ALTA  
**Impacto:** Violação de LGPD/GDPR - PII (CPF, nomes, datas) logadas em produção

**Ação:**
- [ ] Condicionar essas opções apenas a Development
- [ ] Desabilitar em Staging e Production
- [ ] Implementar data masking nos logs

---

### 3. SEGURANÇA ALTA: Dados PII Expostos em Respostas

**Arquivos:**
- `/src/main/Application/v1/Profile/Common/Models/ProfileResponse.cs`
- `/src/main/Api/Grpc/Mapping/Profiles/V1/ProfileGrpcMapper.cs`

**Problema:** CPF retornado completo em todas as respostas API sem mascaramento

**Severidade:** 🟠 ALTA  
**Impacto:** Violação de privacidade - exposição de identificador único pessoal

**Ação:**
- [ ] Criar DTOs com CPF mascarado (XXX.XXX.XXX-**)
- [ ] Restringir CPF completo apenas a operações administrativas
- [ ] Separar modelos por contexto (público vs. administrativo)

---

### 4. SEGURANÇA ALTA: Validação de CPF Inadequada

**Arquivos:**
- `/src/main/Api/Grpc/Validation/Profiles/V1/CreateProfileRequestValidator.cs`
- `/src/main/Api/Mcp/Validators/CreateProfileToolInputValidator.cs`

**Problema:** CPF validado apenas com `NotEmpty()`, sem validar formato ou dígitos verificadores

**Severidade:** 🟠 ALTA  
**Impacto:** Entrada de dados inválidos, violação de integridade

**Ação:**
- [ ] Implementar validação de 11 dígitos
- [ ] Validar dígitos verificadores do CPF (algoritmo)
- [ ] Rejeitar CPFs conhecidos como inválidos (111.111.111-11, etc.)

---

### 5. SEGURANÇA ALTA: Erros de Compilação - Sintaxe `extension()` Inválida

**Arquivos:**
- `/src/main/Infrastructure/Extensions/AppConfigurationExtensions.cs` (linha 12)
- `/src/main/Infrastructure/Extensions/DatabaseExtensions.cs` (linha 15)
- `/src/main/Api/Extensions/WebApplicationBuilderExtensions.cs` (linha 9)
- `/src/main/Api/Extensions/OpenTelemetryExtensions.cs` (linha 42)
- `/src/main/Api/Mcp/Extensions/ResultExtensions.cs` (linhas 11, 35)

**Problema:** Sintaxe `extension()` não é válida em C#

**Severidade:** 🟠 ALTA  
**Impacto:** Código não compila - aplicação quebrada

**Ação:**
- [ ] Remover sintaxe `extension()` de todos os arquivos
- [ ] Usar extension methods padrão com `public static`

---

### 6. PERFORMANCE ALTA: QueryTrackingBehavior Não Otimizado

**Arquivo:** `/src/main/Infrastructure/Extensions/DatabaseExtensions.cs`

**Problema:** Default é `TrackAll` - todas as leituras rastreiam mudanças desnecessariamente

**Severidade:** 🟠 ALTA (Performance)  
**Impacto:** Overhead de memória, alocação de change trackers desnecessária

**Ação:**
- [ ] Trocar default para `NoTrackingWithIdentityResolution`
- [ ] Aplicar `AsNoTracking()` em queries de leitura

---

### 7. PERFORMANCE ALTA: Queries de Paginação Não Otimizadas

**Arquivo:** `/src/main/Infrastructure/Repositories/Profile/ProfileRepository.cs` (linhas 44-48)

**Problema:** Retorna entidades completas com `SELECT *` e mapeia em memória

**Severidade:** 🟠 ALTA (Performance)  
**Impacto:** Transferência desnecessária de dados (`version`, `created_at`, `updated_at`, `deleted_at`)

**Ação:**
- [ ] Implementar projeção direto no banco (`.Select()` no EF Core)
- [ ] Remover campos não utilizados da response
- [ ] Criar `GetPagedProjectedAsync()` sem materializar entidades

---

### 8. PERFORMANCE ALTA: GetById sem Projeção

**Arquivo:** `/src/main/Infrastructure/Repositories/Profile/ProfileRepository.cs` (linhas 20-28)

**Problema:** Retorna entidade completa incluindo `version`, `deleted_at`, `updated_at` desnecessários

**Severidade:** 🟠 ALTA (Performance)  
**Impacto:** Rastreamento desnecessário, overhead de memória

**Ação:**
- [ ] Adicionar método `GetByIdProjectedAsync()` com `.Select()` para leitura pura
- [ ] Manter `GetByIdAsync()` para operações de escrita (activate/inactivate)

---

## 🟠 PROBLEMAS MÉDIOS (Próxima Sprint)

### Segurança - Médios (5 problemas)

| # | Problema | Arquivo | Ação |
|---|----------|---------|------|
| 1 | Sem rate limiting | Multiple endpoints | Implementar AspNetCoreRateLimit para proteção contra DoS |
| 2 | IDOR potencial | GetByIdProfileUseCase | Validar propriedade do recurso antes de retornar |
| 3 | HTTPS não forçado | appsettings.json | Adicionar certificados dev, HTTPS obrigatório |
| 4 | MCP sem autorização | ProfileTools.cs | Adicionar `[Authorize]` aos endpoints MCP |
| 5 | Validação fraca de paginação | ListProfilesRequestValidator | MaximumLength em strings, PageSize bounds |

### Performance - Médios (3 problemas)

| # | Problema | Arquivo | Ação |
|---|----------|---------|------|
| 1 | Índices faltantes | ProfileMapping.cs | Adicionar índices em `status`, `gender`, `full_name` (pg_trgm) |
| 2 | Cursor com UUID não monotônico | ProfilePagedFilterBuilder.cs | Trocar para `created_at` ou usar UUIDv7 |
| 3 | CommandTimeout muito baixo | DatabaseExtensions.cs | Aumentar de 5s para 30s com retry explícito |

---

## 🟡 PROBLEMAS BAIXOS (Melhorias)

### Segurança - Baixos (3 problemas)
1. ApiKeyProvider sem validação real contra banco
2. Headers de segurança HTTP faltando (HSTS, CSP, X-Frame-Options)
3. Validação de tamanho de strings inconsistente

### Testes - Crítico (19 classes sem cobertura)

---

## 📋 PLANO DE AÇÃO PRIORIZADO

### ✅ FASE 1: Bloqueadores Críticos (Semana 1)
**Impacto:** Evitar vazamento de credenciais e falhas de compilação

1. **[1h]** Corrigir erros de compilação (sintaxe `extension()`)
   - 5 arquivos, busca/replace simples
   - Validar que a solução compila

2. **[2h]** Remover credenciais hardcoded
   - Remover dados de `appsettings.Development.json`
   - Documentar uso de `dotnet user-secrets`
   - Adicionar `.gitignore`

3. **[1h]** Desabilitar sensitive logging em produção
   - Condicionar `EnableSensitiveDataLogging` a Development apenas
   - Validar logs em staging

**Resultado esperado:** Código compila, credenciais protegidas, dados sensíveis não logados em produção

---

### 📋 FASE 2: Segurança de Dados (Semana 2)
**Impacto:** Conformidade LGPD/GDPR, validação de entrada

4. **[3h]** Implementar mascaramento de CPF
   - Criar DTOs separados (público vs. administrativo)
   - Atualizar mappers
   - Testar em endpoints

5. **[2h]** Validação completa de CPF
   - Implementar verificação de dígitos
   - Rejeitar CPFs inválidos conhecidos
   - Adicionar testes unitários

6. **[1.5h]** Segurança dos endpoints MCP
   - Adicionar `[Authorize]` aos tools
   - Testar que endpoints não-autenticados são bloqueados

**Resultado esperado:** Dados PII protegidos, validação robusta, endpoints seguros

---

### ⚡ FASE 3: Performance (Semana 3)
**Impacto:** Redução de latência, otimização de queries

7. **[2h]** Otimizar tracking behavior
   - Trocar default para `NoTrackingWithIdentityResolution`
   - Adicionar `AsNoTracking()` em reads

8. **[3h]** Projeções de EF Core
   - `GetPagedProjectedAsync()` com `.Select()`
   - `GetByIdProjectedAsync()` para leitura
   - Validar impacto de performance

9. **[1h]** Índices PostgreSQL
   - Criar índices em `status`, `gender`
   - Criar índice pg_trgm para `full_name`

10. **[1h]** Ajustar timeouts
    - CommandTimeout: 5s → 30s
    - Retry: explícito 3x com backoff

**Resultado esperado:** Redução de latência p95 >20%, uso de memória -15%

---

### 🧪 FASE 4: Testes Unitários (Semana 4-5)
**Impacto:** Cobertura <5% → 60%+, confiança em refatoração

**Prioridade P1** (19 classes, ~50h de testes):
- Domain Entity (Profile)
- UseCases (Create, Activate, Inactivate, GetById, GetPaged)
- Validators (Create, Activate, Inactivate, Get, MCP)
- Interceptors (Exception, Validation)

**Prioridade P2** (6 classes, ~15h):
- Extensions (ResultTranslator, ResultGrpcExtensions, LocationBuilder)
- Mappers (ResultTypeGrpcStatusCodeMapper)
- Handlers (GlobalExceptionHandler, GuidGenerator)

**Resultado esperado:** Cobertura 60%+, confiança em deploys

---

### 🔒 FASE 5: Hardening Adicional (Semana 6)
**Impacto:** Defesa em profundidade

- [ ] Rate limiting (AspNetCoreRateLimit)
- [ ] Validação IDOR (verificar propriedade de recursos)
- [ ] Headers de segurança HTTP
- [ ] Revogação de API keys

---

## 📊 MATRIZ DE RISCOS

```
SEVERIDADE vs. ESFORÇO

Alto Esforço │
             │  
             │ Testes [Phase 4]    │ HTTPS [Phase 5]
             │ (19 classes, 60h)   │ (1h)
             │                     │
Médio Esforço│ Performance [P3]    │ Índices [P3]
             │ (8h)                │ (1h)
             │                     │
Baixo Esforço│ Erros Compilação[P1]│ API Key [P5]
             │ (1h)                │ (2h)
             ├─────────────────────┼──────────────
             Crítico/Alto          Médio/Baixo
                IMPACTO
```

**Path crítico:** P1 (Blockers) → P2 (Security) → P3 (Performance) → P4 (Tests) → P5 (Hardening)

---

## 🎯 PRÓXIMOS PASSOS IMEDIATOS

### Hoje (Semana de 06/05/2026):

1. **[30 min]** Criar branch `fix/security-critical`
   ```bash
   git checkout -b fix/security-critical
   ```

2. **[1h]** Corrigir erros de compilação
   - Arquivos: `AppConfigurationExtensions.cs`, `DatabaseExtensions.cs`, etc.
   - Remover `extension()`, manter assinatura como `public static`

3. **[1h]** Remover credenciais
   - Remover do `appsettings.Development.json`
   - Criar `.env.example` com placeholders

4. **[30 min]** Commit e PR para review

### Próximos 3 dias:

5. Implementar mascaramento de CPF
6. Validação de CPF com dígitos verificadores
7. Desabilitar sensitive logging em produção
8. Autorização nos endpoints MCP

### Próximas 2 semanas:

9. Fase 3: Performance (EF Core)
10. Iniciar Fase 4: Testes (foco nas classes críticas)

---

## 📎 REFERÊNCIAS

**Problemas EF Core:** 6 achados, 3 alto-impacto, arquivo crítico `/src/main/Infrastructure/Extensions/DatabaseExtensions.cs`

**Problemas Segurança:** 13 achados, 5 alto-impacto, bloqueadores em `/src/main/Api/appsettings.Development.json`

**Problemas Testes:** 19 classes sem cobertura, estrutura detalhada abaixo de `/src/main/` espelhada em `/src/tests/unit/Api.UnitTests/`

---

## ✅ CHECKLIST DE CONCLUSÃO

### Fase 1 - Bloqueadores Críticos
- [ ] Sintaxe `extension()` corrigida em 5 arquivos
- [ ] Código compila sem erros
- [ ] appsettings.Development.json sem credenciais
- [ ] `.gitignore` atualizado
- [ ] Testes passam

### Fase 2 - Segurança de Dados
- [ ] CPF mascarado em responses públicas
- [ ] Validação de CPF com checksum implementada
- [ ] Endpoints MCP com `[Authorize]`
- [ ] Testes de segurança adicionados

### Fase 3 - Performance
- [ ] QueryTrackingBehavior otimizado
- [ ] Projeções implementadas (GetPagedProjected, GetByIdProjected)
- [ ] Índices criados em status, gender, full_name
- [ ] Benchmarks antes/depois

### Fase 4 - Testes (19 classes)
- [ ] Domain entities testadas (5 classes)
- [ ] UseCases testados (6 classes)
- [ ] Validators testados (5 classes)
- [ ] Interceptors testados (2 classes)
- [ ] Cobertura ≥ 60%

### Fase 5 - Hardening
- [ ] Rate limiting implementado
- [ ] Headers de segurança HTTP adicionados
- [ ] IDOR validações implementadas
- [ ] Security review final

---

**Status:** 🔴 BLOQUEADO (Erros de compilação + Credenciais expostas)  
**Próximo:** Executar Fase 1 imediatamente  
**Deadline recomendado:** 2026-06-12 para Fase 1-2

