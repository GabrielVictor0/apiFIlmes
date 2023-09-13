    using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o servico de controller 
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options=>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         //valida quem está solicitando
         ValidateIssuer = true,

         //valida quem está recebendo
         ValidateAudience = true,
         
         //define se o tempo de expiração será válido
         ValidateLifetime = true,

         //forma de criptografia e valida a chave de autenticação
         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

         //valida o tempo de expiração do token
         ClockSkew = TimeSpan.FromMinutes(5),

         //nome do issuer (de onde está vindo)
         ValidIssuer = "webapi.Filmes",

         //nome do audience (para onde está indo)
         ValidAudience = "webapi.Filmes"
     };
 });

//Adicione o gerador do swagger à coleção de serviços
builder.Services.AddSwaggerGen(options =>
{   
    //adiciona informações sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API filme manhã",
        Description = "API para gerenciamento de filmes - Introdução da sprint 2 - Backend api",
        TermsOfService = new Uri("https://github.com/GabrielVictor0"),
        Contact = new OpenApiContact
        {
            Name = "Senai Informática",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // Configura  o swagger para usar o arquivo xml gerado
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //Usando a autenticaçao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

var app = builder.Build();

//Comeca a config do swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

//Finaliza a config do swagger 

//Adiona o mapeamento da controller 
app.MapControllers();

//adiciona autenticação
app.UseAuthentication();

//adiciona autorização
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
