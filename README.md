# oAuth2 Library Project 🚀

[![Build Status](https://img.shields.io/github/actions/workflow/status/RunGuitarMan/oAuth2/publish.yml?branch=main)](https://github.com/RunGuitarMan/oAuth2/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](https://opensource.org/licenses/MIT)
[![NuGet Version](https://img.shields.io/nuget/v/OAuth2.Library?style=flat-square)](https://www.nuget.org/packages/RunGuitarMan.oAuth2)

Сейчас проект находится в стадии **init** 🔰

## 🎯 Цель проекта

Цель проекта — глубокое изучение флоу аутентификации OAuth2 🔐 и возможностей создания библиотек для использования в своих проектах 💡.

## 📚 Описание

Данный проект представляет собой шаблон для создания универсальной библиотеки OAuth2 на платформе .NET. Основные функции:

- **Генерация, валидация и обновление JWT-токенов** 🔑
- **Поддержка различных grant-типов** (authorization code, client credentials, password, refresh token, implicit) 🔄
- **Универсальное хранилище токенов** (in-memory, база данных или иной подход) 💾
- **Интеграция с ASP.NET Core** через middleware и обработчики аутентификации 🌐
- **Легкость тестирования и расширения** благодаря четко определённым интерфейсам и архитектуре SOLID 📐

## 🗂 Структура проекта

```plaintext
OAuth2.sln
├── OAuth2.Core
│   ├── Configuration
│   │   ├── OAuth2Options.cs       // Конфигурационные параметры
│   │   └── OAuth2Configuration.cs // Обертка для опций (IOAuth2Configuration)
│   ├── Extensions
│   │   └── OAuth2ServiceCollectionExtensions.cs // Методы расширения для DI
│   ├── Helpers
│   │   ├── OAuth2Utils.cs         // Вспомогательные утилиты (генерация токенов, хэширование и т.д.)
│   │   └── TokenValidator.cs      // Валидация JWT-токенов
│   ├── Interfaces
│   │   ├── IAuthorizationService.cs
│   │   ├── IClientService.cs
│   │   ├── IOAuth2Configuration.cs
│   │   ├── IOAuth2TokenStore.cs
│   │   └── ITokenService.cs
│   ├── Middlewares
│   │   └── OAuth2Middleware.cs    // Middleware для проверки токенов
│   ├── Models
│   │   ├── OAuth2Client.cs        // Модель клиента OAuth2
│   │   ├── OAuth2Error.cs         // Модель для ошибок OAuth2
│   │   ├── OAuth2Scope.cs         // Модель области доступа (scope)
│   │   ├── OAuth2Token.cs         // Модель токена (access и refresh токены)
│   │   ├── OAuth2User.cs          // Модель пользователя (при необходимости)
│   │   └── GrantType.cs           // Перечисление типов grant
│   └── Services
│       ├── AuthorizationService.cs // Реализация IAuthorizationService
│       ├── ClientService.cs        // Реализация IClientService
│       ├── DatabaseTokenStore.cs   // Реализация IOAuth2TokenStore для БД
│       ├── InMemoryTokenStore.cs   // Реализация IOAuth2TokenStore для in-memory хранения
│       └── TokenService.cs         // Реализация ITokenService
└── OAuth2.AspNetCore
    ├── OAuth2AuthenticationHandler.cs    // Обработчик аутентификации для ASP.NET Core
    ├── OAuth2MiddlewareExtensions.cs     // Методы расширения для middleware
    └── OAuth2OptionsSetup.cs              // Настройка опций для аутентификации