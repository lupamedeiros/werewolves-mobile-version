# Werewolves of Millers Hollow - Mobile Version

Link com as regras do jogo: https://cdn.svc.asmodee.net/production-asmodeeca/uploads/2023/07/WerewolvesThePact_EN_Rules.pdf

## ⚠️ Aviso Importante

**Este projeto é destinado apenas para fins educacionais.**

Esta versão digital do jogo "Werewolves of Millers Hollow" foi criada como um exercício de aprendizado e não é uma adaptação oficial do jogo original. O uso comercial deste projeto, incluindo mas não se limitando a venda, distribuição ou licenciamento, é estritamente proibido sem a permissão dos detentores dos direitos do jogo original.

Por favor, respeite os direitos de propriedade intelectual dos criadores originais de "Werewolves of Millers Hollow". Se você deseja criar um produto comercial, considere entrar em contato com os detentores dos direitos para obter as permissões necessárias.

**Resumindo: Este projeto é apenas para aprendizado e diversão. Não use para ganhar dinheiro ou distribuí-lo sem permissão.**

## 📖 Sobre o Projeto

Este é um projeto de estudo para desenvolver uma versão digital mobile do jogo "Werewolves of Millers Hollow", utilizando Unity, Photon e SQLite. O jogo é um clássico de dedução social, onde jogadores são divididos em dois grupos: Lobisomens e Aldeões. O objetivo é eliminar o grupo adversário por meio de estratégia, blefe e cooperação.

> **Aviso:** Este projeto é apenas para fins educacionais e não está autorizado oficialmente pelos detentores dos direitos do jogo "Werewolves of Millers Hollow".

## 🛠️ Tecnologias Utilizadas

- **Unity**: Para o desenvolvimento do jogo e da interface do usuário.
- **Photon**: Para implementar a funcionalidade multiplayer e permitir a comunicação entre jogadores em tempo real.
- **SQLite**: Para armazenar dados locais, como configurações de jogo, pontuações e estatísticas.

## 📲 Funcionalidades

- **Multiplayer em tempo real**: Permite que jogadores se conectem e joguem juntos online.
- **Atribuição automática de papéis**: Distribui papéis de maneira aleatória no início do jogo.
- **Fases automatizadas de Dia e Noite**: Sistema que gerencia turnos de jogo automaticamente.
- **Sistema de votação e chat**: Permite que os jogadores discutam e votem durante a fase diurna.
- **Armazenamento local de dados**: Configurações e estatísticas são salvas localmente no dispositivo do jogador.

## 📋 Pré-requisitos

- **Unity 2021.3 ou superior**
- **Conta Photon**: Crie uma conta no site do [Photon](https://www.photonengine.com/) para obter um `App ID`.
- **SQLite**: Incluído como parte do projeto Unity.

## 🚀 Como Configurar o Projeto

1. **Clone este repositório**:
2. 
    ```bash
    git clone https://github.com/seu-usuario/werewolves-mobile.git
    ```
3. **Abra o projeto no Unity**
4. **Instale o Photon PUN 2**: Disponível na Unity Asset Store.
5. **Configure o Photon**:
   - Crie uma conta no Photon e obtenha seu `App ID`.
   - No Unity, acesse `Window > Photon Unity Networking > PUN Wizard`.
   - Insira seu `App ID` e conecte ao Photon.
6. **Compilar e rodar o projeto no dispositivo**.

## ⚙️ Estrutura do Projeto

```plaintext
/Assets
  /Scripts         # Scripts de lógica do jogo
  /Resources       # Assets do jogo (imagens, sons, etc.)
  /Scenes          # Cenas do jogo (Menu, Jogo, Lobby)
  /Plugins         # Bibliotecas externas, incluindo Photon e SQLite
```
