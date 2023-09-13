# SoftWell.RtCodes.Abstractions

Абстракции и базовые классы для преобразования кодов из одной схемы в другую.

Все коды получаются из списка зарегистрированных [источников](./ICodesSource.cs).

[CodesConverter](./CodesConverter.cs) регистрируется как IHostedService, на StartAsync запрашивает у всех источников коды, сохраняет их в словарь, и оттуда уже выдает.

Везде, где нужна конвертация кодов, следует инжектить [ICodesConverter](./ICodesConverter.cs).


## Источники

В базовом пакете есть возможность подключать источники из файлов на диске и из файлов, полученных по http.
Для каждого файла нужен свой [читатель](./Sources/ICodesStreamReader.cs), который умеет читать коды из потока.


## Использование

```c#
var section = GetConfiguration().GetSection("CodesSectionName");
services.AddCodesConversion(
    opts => 
        opts.AddSource(GetMySource()) // кастомный источник
            .AddFileSource("some-path", GetMyCodesReader()) // коды из файла на диске
            .AddHttpSource("https://some-url/...", GetMyCodesReader()) // коды, полученные по http
        );
```