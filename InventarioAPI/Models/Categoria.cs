﻿namespace InventarioAPI.Models;

public class Categoria
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public bool Situacao { get; set; }
    public string? ImageUrl { get; set; }
}
