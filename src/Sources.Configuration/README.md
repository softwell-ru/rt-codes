# SoftWell.RtCodes.Configuration

Получение маппинга кодов из конфигурации.
Остался на всякий случай просто потому, что был написан. Вообще, словари с ключами, содержащими двоеточие, из конфига читаются криво, поэтому пришлось придумывать такую структуру, но и она не особо удобна.


## Использование


```c#
var section = GetConfiguration().GetSection("CodesSectionName");
services.AddCodesConversion(
    opts => opts.AddConfigurationSource(section));
```

Пример конфигурации в json:

```json
{
    "CodesSectionName": {
        "Schemes": [
            {
                "Scheme": "https://hihiclub.ru/coding-schemes/partner",
                "Codes": [
                    {
                        "Code": "SOFT",
                        "Mapping": [
                            {
                                "Scheme": "some-party-scheme",
                                "Code": "111"
                            }
                        ]
                    },
                    {
                        "Code": "WELL",
                        "Mapping": [
                            {
                                "Scheme": "some-party-scheme",
                                "Code": "222"
                            },
                            {
                                "Scheme": "some-other-party-scheme",
                                "Code": "333"
                            }
                        ]
                    }
                ]
            },
            {
                "Schema": "https://hihiclub.ru/coding-schemes/instrument-id",
                "Codes": [
                    {
                        "Code": "USD/RUB Fx Tod",
                        "Mapping": [
                            {
                                "Scheme": "some-product-scheme",
                                "Code": "USD_RUB_TOD"
                            },
                            {
                                "Scheme": "some-other-product-scheme",
                                "Code": "USD-RUB-TOD"
                            }
                        ]
                    }
                ]
            }
        ]
    }
}
```
