using System.Collections.Generic;
using UnityEngine;

namespace AIO.RainbowCore
{
	internal static class CoreEditorTexturesStorage
	{
		private static readonly Dictionary<CoreEditorTexture, Texture2D> EDITOR_TEXTURES = new Dictionary<CoreEditorTexture, Texture2D>();

		private static readonly Dictionary<CoreEditorTexture, string> EDITOR_STRINGS = new Dictionary<CoreEditorTexture, string>
		{
			{
				CoreEditorTexture.IcnFoldoutFirst,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAARElEQVQ4EWNMTk5moAQwUaIZpJcYA/7js4QYA/DpJ8oFw90AFrweREhiiwlGkDSxBoAVI8xDsEajkbi8gAgxLCyKAxEASaMDTZdoapIAAAAASUVORK5CYII="
			},
			{
				CoreEditorTexture.IcnFoldoutMiddle,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAOElEQVQ4EWNMTk5mIAD+A+UZcalhwiVBrPioAQwMAx8GLERGFygtoANw2iDWgNGEhB58SPyBTwcAzwkDTjZ7Lg4AAAAASUVORK5CYII="
			},
			{
				CoreEditorTexture.IcnFoldoutLast,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAOklEQVQ4EWNMTk5mIAD+A+UZcalhwiVBrPioAQwMAx8GLERGFygtoANw2iDGAJyJCGTiwIfBqAsYGADPDQNOqygRVQAAAABJRU5ErkJggg=="
			},
			{
				CoreEditorTexture.IcnFoldoutLevels,
				"iVBORw0KGgoAAAANSUhEUgAAAIAAAAAQCAYAAADeWHeIAAAAu0lEQVRoBe2WwQ2AMAwDgTFYgzmYohJzsAZzsAZzMAffPPKxqqiyOF5J5FapHRHPrbWJ778MLMLTVwEboVtMhHgXsBF6xESITwEboVdMhPgWsBH6xESI3wyrDEB2npo5AzMrwFzBzvb5A3QS6H5cGQA8QK42HiDnhaoDA3gAB5UKe1RWQGEbXD2KAWUA8AC5SniAnBeqDgzgARxUKuxRWQGFbXD1KAaUAcAD5CrhAXJeqDowgAdwUKmwxw9Hahfhli/h/wAAAABJRU5ErkJggg=="
			},
			{
				CoreEditorTexture.IcnFilter,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABOklEQVQ4Ed1TPUsDQRB9c7fxogZBbKxEEUv9G2JlYRP/gVEE/4K/IpWVGEiRIpW9dhY2FoKkCAliKtEgUXMf40yOS+68O5PagR3efLy3O8suXZ1toGATvGEAZsxkRIBxLLg+wwhjRlq2trl9fEP1+iG7OiVb2dsGHe0uaxtXmwfgwSoQdEErL+BXH3j2wB8B0GdwrwBYDJrXbgcnF3fKI0u9GFX2G6CFnqAtYLAkWEoyKwJxvpCNAyrNJchKjAQUhyKLbfDnDlAUogq44r5sUFGuyqPxzkpQiwtoHIqUOsD3GtgTkgr4tpwEKXKWwFhEAYay3HCPaOZRPuaodroeCxOQy+eb4JaL+qWcKBwo0aDB7xFSDdMS/0DA3Dy9545Zzq1MCn/dAR0f3k86c1D0F3LKo3T0W/VdpuwHBT9ZSPFSJXsAAAAASUVORK5CYII="
			},
			{
				CoreEditorTexture.IcnDelete,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAB4UlEQVQ4EZVTv0ubURQ9L/kSN4U66JClhQyFCqa7cIOCgyAOxbVDJzcn1+/z33Fy0U1fQHAqRip0EZyExCGFChrzq+k57/WTT2qHXrjvnHvvuff9+BI3+dKErFQuB9TyazLBeDzGZDQKuXKlgiRJ/tKoWAqKuGSEKd3o/zJjQZqMHiwfkHHXFGtr2t2zYrH8YrVQi5qUlUxVN/i8IpxWNzYAHZnHHR4dKdfkFVoivIIR/CsaV9I96c3+4SEwHAIPD5CQufwkJh6aWZNGWvXQ4X5++qhNZEb3s6ur4sD8PO4PDgKd3d4Ger3A709OhHr5lojrbX4Q5mYk/o0JaLVaxNvbgD9aLeFzswLXXa8Li2YM/MJKeJvn/N3ZmfiLZiWSXqcjRKVaRUKvzMyEGHNzQLcb+eIiRnof2mgwwJg8j/PPGIV8Bwp8bWcHuLkBHh+jkyunGoWWi4XFAcbJ/u3uLnB6CvT7uOax5eLKqSYN+0zNsnyA8Ui+vrcHHB8DT0/4fn6uYzbl4sqpJo207DUNcN/ehztPl/b3Af0WaFfttqD4YMbYLzUaygNbW7hKUzHnLurhT5QxSBvLy2hfXqpQbFYsM7ovaLgjMvf1HSFaRtDY15qj4s8QBqFZyeKAXPRf+BuPJd+wTIu7wgAAAABJRU5ErkJggg=="
			},
			{
				CoreEditorTexture.IcnSettings,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACOklEQVQ4EU1TXctpURAe7Bf5VkjCe1wpcUEuFPUi/8HPsd34Bf7A+R/KeePChXLhQqLcIPmWFPnYZ5459tuZmrWeNTPPrNmzZhuazSZBTCaT7G63m87nM7DKWgNgqbOqTqeTTqeTGJ7Pp+xGWf8tKm9twM/PT3q9XrVKpUJQYNjeghhVP+gJVM5Yy2QyRXZoUMakaRrIBPyfvYhYPqswKu9SvrLZLPl8PnK5XLTf72mz2QhGELDX65XKzGYzpdNp6vf7X/AZGo0GhUIhYA1JPj4+CEnRE70vOOu2+/0OsnCXyyUpQADxeJzW6zX5/X6yWCzU6/Wo2+3CTYVCgXK5HN1uN6kGzRyPx+JDD1RWDUaUiltxA+sfq9VqgALDBh9iEAsOq2p8PB61arVKwWCQIpEIKYpCw+GQ7HZ7iRW7YNjgQwxiwQFXwTfN53Nk/BHcACLfLrbr9UqXy0Uw4g+HgyiwKZ/PG6bTaRHdXywWFAgEyOFw0G63K/KNv8Him9rJZPIXEqISNLTT6eCJ69KDaDRq2G63QsSkYWgSiUSRuTITwLDBh+SIBYf9qoLSRqORzEA4HJYXWK1W8u6YQghKhs1ms0mCyWQiSeBTsLC00Ry+SUoE4Xg8UiwWE+dsNiOPxyNJU6mUkLmKNjtL+iR+8zNhRAnBmDoQ+NslAZrKPSHulZAHgwHs31j0BCoOnATjWUIwi1Yul7HLXPCGb0Yi3AyyyvrzCcAqFvw8b6m3Wq2ajnWj0Wgs6Rj7X09oK3tAFnhwAAAAAElFTkSuQmCC"
			}
		};

		public static Texture2D GetTexture(CoreEditorTexture type)
		{
			return CoreTexturesStorageHelper<CoreEditorTexture>.GetTexture(type, FilterMode.Point, EDITOR_STRINGS, EDITOR_TEXTURES);
		}
	}
}
