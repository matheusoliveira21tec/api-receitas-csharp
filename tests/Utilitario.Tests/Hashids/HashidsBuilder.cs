﻿namespace Utilitario.Tests.Hashids;
public class HashidsBuilder
{
    private static HashidsBuilder _instance;
    private readonly HashidsNet.Hashids _encripter;

    private HashidsBuilder()
    {
        if (_encripter is null)
            _encripter = new HashidsNet.Hashids("qj328TtMDy", 3);
    }

    public static HashidsBuilder Instance()
    {
        _instance = new HashidsBuilder();
        return _instance;
    }

    public HashidsNet.Hashids Build()
    {
        return _encripter;
    }
}