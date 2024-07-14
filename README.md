<h1>Teste Técnico - Creatus</h1>

<h2>Descrição do Projeto</h2>
<p>
    Este projeto foi desenvolvido como parte do teste técnico para a empresa Creatus. Consiste em uma WebAPI construída em C# usando Entity Framework e SQLite. A API é responsável por gerenciar usuários, com autenticação JWT e geração de relatórios em PDF.
</p>

<h2>Motivação e Tecnologias Utilizadas</h2>

<h3>Entity Framework (ORM)</h3>
<p>
    Utilizamos o Entity Framework como Object-Relational Mapping (ORM). O ORM facilita a interação com o banco de dados, permitindo que trabalhemos com objetos em vez de escrever SQL diretamente. Isso torna o desenvolvimento mais intuitivo e menos propenso a erros, além de melhorar a manutenção do código.
</p>

<h3>SQLite</h3>
<p>
    Optamos pelo SQLite devido à sua leveza e simplicidade. É um banco de dados relacional que não requer uma instalação complexa, sendo ideal para projetos menores e testes.
</p>

<h3>Minimal API</h3>
<p>
    Adotamos a Minimal API por ser uma abordagem mais recente no .NET, ideal para projetos pequenos e rápidos. Ela permite criar endpoints de forma mais direta e simplificada. Mesmo com a simplicidade da Minimal API, mantivemos a organização do projeto, estruturando-o em pastas.
</p>

<h3>JWT para Autenticação</h3>
<p>
    Para garantir a segurança, utilizamos JSON Web Tokens (JWT) para autenticação. Isso permite que apenas usuários autenticados e autorizados acessem determinados endpoints.
</p>

<h3>QuestPDF para Geração de Relatórios</h3>
<h2>Endpoints</h2>

<h3>Autenticação</h3>
<ul>
    <li><code>POST /users/login</code>: Autentica um usuário e retorna um token JWT.</li>
</ul>

<h3>Usuários</h3>
<ul>
    <li><code>POST /users</code>: Cria um novo usuário.</li>
    <li><code>GET /users</code>: Retorna todos os usuários ativos.</li>
    <li><code>GET /users/{id}</code>: Retorna detalhes de um usuário específico.</li>
    <li><code>PUT /users/{id}</code>: Atualiza um usuário específico.</li>
    <li><code>DELETE /users/{id}</code>: Deleta um usuário específico.</li>
</ul>

<h3>Relatórios</h3>
<ul>
    <li><code>GET /users/report</code>: Gera um relatório em PDF de todos os usuários ativos. (Acessível apenas para usuários acima do level 4 logados)</li>
</ul>

<h2>Como Executar o Projeto</h2>
<ol>
    <li>Clone o repositório:
        <pre><code>git clone https://github.com/SeuUsuario/SeuRepositorio.git
cd SeuRepositorio</code></pre>
    </li>
    <li>Instale as dependências:
        <pre><code>dotnet restore</code></pre>
    </li>
    <li>Execute as migrações para criar o banco de dados:
        <pre><code>dotnet ef database update</code></pre>
    </li>
    <li>Execute o projeto:
        <pre><code>dotnet run</code></pre>
    </li>
    <li>Acesse a API via <code>https://localhost:5001/swagger</code> para testar os endpoints.</li>
</ol>

<h2>Considerações Finais</h2>
<p>
    Este projeto foi desenvolvido com foco na simplicidade e organização, utilizando tecnologias modernas e boas práticas de desenvolvimento. A autenticação JWT e a geração de relatórios em PDF garantem a segurança e a funcionalidade solicitadas pela empresa Creatus.
</p>
<p>
    Para a geração de relatórios em PDF, utilizamos a biblioteca QuestPDF. Essa biblioteca facilita a criação de documentos PDF de maneira programática, permitindo customizações detalhadas no layout do documento.
</p>
